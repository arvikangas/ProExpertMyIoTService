using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using MyIoTService.Core.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Services.Mqtt
{
    public class MqttService : IMqttService
    {
        private readonly IManagedMqttClient _client;
        private readonly ManagedMqttClientOptions _options;

        public MqttService(IOptions<MqttOptions> mqttOptions)
        {
            // Setup and start a managed MQTT client.
            _options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId(mqttOptions.Value.ClientId)
                    .WithTcpServer(mqttOptions.Value.Server, 1883)
                    .Build())
                .Build();

            _client = new MqttFactory().CreateManagedMqttClient();

            _client.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
                Console.WriteLine();
            });
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _client.StartAsync(_options);
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
    }
}
