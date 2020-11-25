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
        private readonly DeviceState _deviceState;

        public MqttHub(IMqttService mqttService, DeviceState deviceState)
        {
            _mqttService = mqttService;
            _deviceState = deviceState;
        }

        public async Task Connect(string device, string password)
        {
            _deviceState.DeviceId = device;
            _deviceState.Password = password;
            await _mqttService.Connect(device, password);
        }
        public async Task Receive(string device, string command, string value)
        {
            await Clients.All.SendAsync("Receive", device, command, value);
        }

        public async Task Send(string command, string value)
        {
            await _mqttService.Send(_deviceState.DeviceId, command, value);
        }
    }
}
