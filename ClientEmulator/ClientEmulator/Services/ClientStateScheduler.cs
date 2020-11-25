using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientEmulator.Services
{
    public class ClientStateScheduler : BackgroundService
    {
        readonly DeviceState _deviceState;

        Timer _timer;
        int tickCounter = 0;

        public ClientStateScheduler(DeviceState deviceState)
        {
            _deviceState = deviceState;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(Callback, null, 0, 1000);

            return Task.CompletedTask;
        }

        private void Callback(object state)
        {
            _deviceState.OperationTimeInSec++;
            if(_deviceState.OperationTimeInSec % 3600 == 0)
            {
                _deviceState.WorkingHour++;
            }
        }
    }
}
