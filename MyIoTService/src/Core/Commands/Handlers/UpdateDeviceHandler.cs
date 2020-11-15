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
            else
            {
                return;
            }

            if(request.HasOutsideTemperatureSensor is { })
            {
                await _mqttService.Send(device.Id, 1010, request.HasOutsideTemperatureSensor.Value ? "1" : "0");
                device.HasOutsideTemperatureSensor = request.HasOutsideTemperatureSensor.Value;
            }

            if (request.OperationTimeInSec is { })
            {
                await _mqttService.Send(device.Id, 2000, request.OperationTimeInSec.Value.ToString());
                device.OperationTimeInSec = request.OperationTimeInSec.Value;
            }

            if (request.WorkingHour is { })
            {
                await _mqttService.Send(device.Id, 2010, request.WorkingHour.Value.ToString());
                device.WorkingHour = request.WorkingHour.Value;
            }

            if (request.IsOperational is { })
            {
                await _mqttService.Send(device.Id, 1010, request.IsOperational.Value ? "1" : "0");
                device.IsOperational = request.IsOperational.Value;
            }

            if (request.SilentMode is { })
            {
                await _mqttService.Send(device.Id, 1010, request.SilentMode.Value ? "1" : "0");
                device.SilentMode = request.SilentMode.Value;
            }

            if (request.SerialNumber is { })
            {
                await _mqttService.Send(device.Id, 1010, request.SerialNumber.Value.ToString());
                device.SerialNumber = request.SerialNumber.Value;
            }
        }
    }
}
