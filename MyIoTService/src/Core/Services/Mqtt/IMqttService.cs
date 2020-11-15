using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIoTService.Core.Services.Mqtt
{
    public interface IMqttService : IHostedService
    {
        Task SubscribeTopic(string topic);
        Task SubscribeDevice(string topic);
        Task UnSubscribeTopic(string topic);
    }
}
