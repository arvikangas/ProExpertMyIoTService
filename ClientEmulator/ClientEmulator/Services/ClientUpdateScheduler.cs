using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientEmulator.Services
{
    public class ClientUpdateScheduler : BackgroundService
    {
        readonly IMqttService _mqttService;
        readonly DeviceState _deviceState;

        Timer _timer;
        int tickCounter = 0;

        public ClientUpdateScheduler(IMqttService mqttService, DeviceState deviceState)
        {
            _mqttService = mqttService;
            _deviceState = deviceState;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(Callback, null, 0, 1000);

            return Task.CompletedTask;
        }

        private async void Callback(object state)
        {
            if(!_mqttService.IsConnected())
            {
                return;
            }
            await _mqttService.Send(_deviceState.DeviceId, "2000", _deviceState.OperationTimeInSec.ToString());
            if(tickCounter % 10 == 0)
            {
                await _mqttService.Send(_deviceState.DeviceId, "2010", _deviceState.WorkingHour.ToString());
            }
        }
    }
}
