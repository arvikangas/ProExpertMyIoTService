using ClientEmulator.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientEmulator.Services
{
    public class MqttService : IMqttService
    {
        private readonly IManagedMqttClient _client;
        private readonly MqttOptions _mqttOptions;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly DeviceState _deviceState;
        private readonly ILogger<MqttService> _logger;

        public MqttService(
            IOptions<MqttOptions> mqttOptions,
            ILogger<MqttService> logger,
            DeviceState deviceState,
            IServiceScopeFactory serviceScopeFactory)
        {
            _mqttOptions = mqttOptions.Value;
            _logger = logger;
            _deviceState = deviceState;

            _serviceScopeFactory = serviceScopeFactory;
            _client = new MqttFactory().CreateManagedMqttClient();

            _client.UseApplicationMessageReceivedHandler(e =>
            {
                HandleMqttMessage(e);
            });

            _client.UseConnectedHandler(e =>
            {
                HandleMqttConnected(e);
            });

            _client.UseDisconnectedHandler(e =>
            {
                HandleMqttDisconnected(e);
            });
        }

        public async Task Connect(string deviceId, string password)
        {
            var options = new ManagedMqttClientOptionsBuilder()
                            .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                            .WithClientOptions(new MqttClientOptionsBuilder()
                                .WithClientId(_mqttOptions.ClientId)
                                .WithTcpServer(_mqttOptions.Server)
                                .WithCredentials(deviceId, password)
                                .Build())
                            .Build();


            if(_client.IsStarted)
            {
                await _client.StopAsync();
            }

            await _client.StartAsync(options);
            await SubscribeTopic($"{_mqttOptions.DevicesTopic}/{deviceId}/receive/#");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.StopAsync();
        }

        public async Task SubscribeTopic(string topic)
        {
            await _client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
        }

        public async Task UnSubscribeTopic(string topic)
        {
            await _client.UnsubscribeAsync(topic);
        }

        private void HandleMqttMessage(MqttApplicationMessageReceivedEventArgs e)
        {
            var topics = e.ApplicationMessage.Topic.Split('/');
            var device = topics[1];
            var command = topics[3];
            var value = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            int dataType;
            if (!int.TryParse(command, out dataType))
            {
                _logger.LogInformation("Invalid format for data type {DataType}", command);
                return;
            }

            if (!AllowedCodes.Contains(dataType))
            {
                _logger.LogInformation("Unknown data type {DataType}", dataType);
                return;
            }

            int payLoad;
            if (string.IsNullOrEmpty(value) || !int.TryParse(value, out payLoad))
            {
                _logger.LogInformation("Invalid payload format. Payload: {Payload}", value);
                return;
            }

            var limits = CodeMinMax[dataType];
            if (payLoad < limits.min || payLoad > limits.max)
            {
                _logger.LogInformation("Payload out of bounds. Payload: {Payload}, min: {Min}, max: {max}", payLoad, limits.min, limits.max);
                return;
            }

            _deviceState.SetProperty(command, payLoad);
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var hub = scope.ServiceProvider.GetService<IHubContext<MqttHub>>();
                hub.Clients.All.SendAsync("Receive", device, command, value);
            }
        }

        private void HandleMqttConnected(MqttClientConnectedEventArgs e)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var hub = scope.ServiceProvider.GetService<IHubContext<MqttHub>>();
                hub.Clients.All.SendAsync("Connected", e.AuthenticateResult.ResultCode.ToString());
            }
        }

        private void HandleMqttDisconnected(MqttClientDisconnectedEventArgs e)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var hub = scope.ServiceProvider.GetService<IHubContext<MqttHub>>();
                hub.Clients.All.SendAsync("Connected", e.ReasonCode.ToString());
            }
        }

        public async Task Send(string device, string dataType, string payload)
        {
            _deviceState.SetProperty(dataType, int.Parse(payload));
            var topic = $"{_mqttOptions.DevicesTopic}/{device}/send/{dataType}";
            var result = await _client.PublishAsync(new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(payload).Build());
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var hub = scope.ServiceProvider.GetService<IHubContext<MqttHub>>();
                await hub.Clients.All.SendAsync("DeviceSend", device, dataType, payload);
            }
        }

        public bool IsConnected()
        {
            return _client.IsConnected;
        }

        static HashSet<int> AllowedCodes = new HashSet<int>
        {
            1000,
            1001,
            1010,
            1011,
            1020,
            2000,
            2010,
            4040,
            4050,
            8000,
            9000
        };

        static Dictionary<int, (int min, int max)> CodeMinMax = new Dictionary<int, (int, int)>
        {
            { 1000, (-150, 150) },
            { 1001, (-100, 100) },
            { 1010, (-0, 1) },
            { 1011, (-100, 100) },
            { 1020, (-100, 100) },
            { 2000, ( 0, int.MaxValue) },
            { 2010, ( 0, int.MaxValue) },
            { 4040, (0, 1) },
            { 4050, (0, 1) },
            { 8000, (0, 1) },
            { 9000, (0, 999999) },
        };
    }
}
