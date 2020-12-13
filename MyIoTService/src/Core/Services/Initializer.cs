using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyIoTService.Core.Repositories;
using MyIoTService.Core.Services.Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Services
{
    public class Initializer : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMqttService _mqttService;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public Initializer(
            IServiceScopeFactory serviceScopeFactory,
            IMqttService mqttService,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mqttService = mqttService;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _hostApplicationLifetime.ApplicationStarted.Register(async () => await OnStarted());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        async Task OnStarted()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var deviceRepository = scope.ServiceProvider.GetService<IDeviceRepository>();
                var devices = (await deviceRepository.FindAllEnabled()).Select(x => x.Id);

                if (!devices.Any())
                {
                    return;
                }

                foreach (var device in devices)
                {
                    await _mqttService.SubscribeDevice(device);
                }
            }
        }
    }
}
