using MediatR;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Services.Mqtt;
using MyIoTService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Commands.Handlers
{
    public class UpdateDeviceHandler : AsyncRequestHandler<UpdateDevice>
    {
        private readonly MyIoTDbContext _db;
        private readonly IMqttService _mqttService;

        public UpdateDeviceHandler(MyIoTDbContext db, IMqttService mqttService)
        {
            _db = db;
            _mqttService = mqttService;
        }

        protected async override Task Handle(UpdateDevice request, CancellationToken cancellationToken)
        {
            var device = await _db.Devices.FindAsync(request.Id);

            device.Enabled = request.Enabled;
            await _db.SaveChangesAsync();

            if(device.Enabled)
            {
                await _mqttService.SubscribeDevice(device.Id);
            }
        }
    }
}
