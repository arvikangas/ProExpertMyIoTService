using MQTTnet.Client.Options;
using System;

namespace ClientEmulator.Services
{
    public class MqttOptions
    {
        public string ClientId { get; set; }
        public string Server { get; set; }
        public string DevicesTopic { get; set; }
    }
}