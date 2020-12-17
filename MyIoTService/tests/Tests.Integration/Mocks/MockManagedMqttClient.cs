using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using MyIoTService.Core.Services.Mqtt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Tests.Integration.Mocks
{
    public class MockManagedMqttClient : IManagedMqttClient
    {
        public IMqttClient InternalClient => throw new NotImplementedException();

        public bool IsStarted => true;

        public bool IsConnected => true;

        public int PendingApplicationMessagesCount => 0;

        public IManagedMqttClientOptions Options => throw new NotImplementedException();

        public IMqttClientConnectedHandler ConnectedHandler { get; set; }
        public IMqttClientDisconnectedHandler DisconnectedHandler { get; set; }
        public IApplicationMessageProcessedHandler ApplicationMessageProcessedHandler { get; set; }
        public IApplicationMessageSkippedHandler ApplicationMessageSkippedHandler { get; set; }
        public IConnectingFailedHandler ConnectingFailedHandler { get; set; }
        public ISynchronizingSubscriptionsFailedHandler SynchronizingSubscriptionsFailedHandler { get; set; }
        public IMqttApplicationMessageReceivedHandler ApplicationMessageReceivedHandler { get; set; }

        public void Dispose() { }

        public Task PingAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

        public Task PublishAsync(ManagedMqttApplicationMessage applicationMessages)
        => Task.CompletedTask;

        public Task<MqttClientPublishResult> PublishAsync(MqttApplicationMessage applicationMessage, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(IManagedMqttClientOptions options)
            => Task.CompletedTask;

        public Task StopAsync()
        => Task.CompletedTask;

        public Task SubscribeAsync(IEnumerable<MqttTopicFilter> topicFilters)
        => Task.CompletedTask;

        public Task UnsubscribeAsync(IEnumerable<string> topics)
        => Task.CompletedTask;
    }
}
