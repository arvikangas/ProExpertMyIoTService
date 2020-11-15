using ClientEmulator.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientEmulator.Hubs
{
    public class MqttHub : Hub
    {
        private readonly IMqttService _mqttService;

        public MqttHub(IMqttService mqttService)
        {
            _mqttService = mqttService;
        }
        public async Task Receive(string device, string command, string value)
        {
            await Clients.All.SendAsync("Receive", device, command, value);
        }

        public async Task Send(string device, string command, string value)
        {
            await _mqttService.Send(device, command, value);
        }
    }
}
