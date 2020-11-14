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
    public class UpdateDeviceHandler : AsyncRequestHandler<CreateDevice>
    {
        private readonly MyIoTDbContext _db;
        private readonly IMqttService _mqttService;

        public CreateDeviceHandler(MyIoTDbContext db, IMqttService mqttService)
        {
            _db = db;
            _mqttService = mqttService;
        }

        protected async override Task Handle(CreateDevice request, CancellationToken cancellationToken)
        {
            await _db
                .Devices
                .AddAsync(new Domain.Device()
                {
                    Id = request.Id
                });

            await _db
                .AccountDevices
                .AddAsync(new Domain.AccountDevice
                {
                    DeviceId = request.Id,
                    AccountId = request.AccountId
                });

            await _db.SaveChangesAsync();

            await _mqttService.SubscribeTopic(request.Id);
        }
    }
}
