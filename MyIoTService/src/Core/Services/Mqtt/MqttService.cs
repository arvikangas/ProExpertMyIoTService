using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Services.Mqtt
{
    public class MqttService : IMqttService
    {
        private readonly IManagedMqttClient _client;
        private readonly MqttOptions _mqttOptions;
        private readonly IMediator _mediator;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly ILogger<MqttService> _logger;

        public MqttService(
            IOptions<MqttOptions> mqttOptions,
            ILogger<MqttService> logger,
            IMediator mediator,
            IServiceScopeFactory serviceScopeFactory,
            IManagedMqttClient managedMqttClient)
        {
            _mqttOptions = mqttOptions.Value;
            _logger = logger;
            _mediator = mediator;
            _serviceScopeFactory = serviceScopeFactory;
            _client = managedMqttClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("MqttService starting");
            await InitializeClient();
            _logger.LogInformation("MqttService started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.StopAsync();
        }

        public async Task SubscribeTopic(string topic)
        {
            await _client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
        }

        public async Task SubscribeDevice(string topic)
        {
            var topicCombined = $"{_mqttOptions.DevicesTopic}/{topic}/send/#";
            await _client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topicCombined).Build());
        }

        public async Task UnSubscribeTopic(string topic)
        {
            await _client.UnsubscribeAsync(topic);
        }

        private async Task InitializeClient()
        {
            var options = new ManagedMqttClientOptionsBuilder()
                            .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                            .WithClientOptions(new MqttClientOptionsBuilder()
                                .WithClientId(_mqttOptions.ClientId)
                                .WithTcpServer(_mqttOptions.Server)
                                .WithCredentials(_mqttOptions.User, _mqttOptions.Password)
                                .Build())
                            .Build();


            await _client.StartAsync(options);

            _client.UseApplicationMessageReceivedHandler(async e =>
            {
                await HandleMqttMessage(e);
            });
        }

        private async Task HandleMqttMessage(MqttApplicationMessageReceivedEventArgs e)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                await mediator.Send(new HandleMqttMessage { Topic = e.ApplicationMessage.Topic, Payload = e.ApplicationMessage.Payload });
            }
        }

        public async Task Send(string deviceId, int dataType, string payload)
        {
            var topic = $"{_mqttOptions.DevicesTopic}/{deviceId}/receive/{dataType}";
            var result = await _client.PublishAsync(new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(payload).Build());
        }
    }
}
