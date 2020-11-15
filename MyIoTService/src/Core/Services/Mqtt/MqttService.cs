using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Options;
using MyIoTService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            IServiceScopeFactory serviceScopeFactory)
        {
            _mqttOptions = mqttOptions.Value;
            _logger = logger;
            _mediator = mediator;
            _serviceScopeFactory = serviceScopeFactory;
            _client = new MqttFactory().CreateManagedMqttClient();
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
            await _client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(GetTopic(topic)).Build());
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
                                .Build())
                            .Build();


            await _client.StartAsync(options);

            _client.UseApplicationMessageReceivedHandler(e =>
            {
                HandleMqttMessage(e);
            });
        }

        private void HandleMqttMessage(MqttApplicationMessageReceivedEventArgs e)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                _mediator.Send(new HandleMqttMessage { Topic = e.ApplicationMessage.Topic, Payload = e.ApplicationMessage.Payload });
            }
        }

        string GetTopic(string deviceId) => $"{_mqttOptions.DevicesTopic}/{deviceId}/#";
    }
}
