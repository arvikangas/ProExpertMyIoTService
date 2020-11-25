using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientEmulator.Services
{
    public interface IMqttService
    {
        Task Connect(string deviceId, string password);
        Task SubscribeTopic(string topic);
        Task Send(string device, string dataType, string payload);
        Task UnSubscribeTopic(string topic);
        bool IsConnected();
    }
}
