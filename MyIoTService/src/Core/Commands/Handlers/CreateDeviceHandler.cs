using MediatR;
using Microsoft.AspNetCore.Http;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Services.Mqtt;
using MyIoTService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Commands.Handlers
{
    public class CreateDeviceHandler : AsyncRequestHandler<CreateDevice>
    {
        private readonly MyIoTDbContext _db;
        private readonly IMqttService _mqttService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHiveMqCredentialsService _hiveMqCredentialsService;

        public CreateDeviceHandler(
            MyIoTDbContext db, 
            IMqttService mqttService,
            IHttpContextAccessor httpContextAccessor,
            IHiveMqCredentialsService hiveMqCredentialsService)
        {
            _db = db;
            _mqttService = mqttService;
            _httpContextAccessor = httpContextAccessor;
            _hiveMqCredentialsService = hiveMqCredentialsService;
        }

        protected async override Task Handle(CreateDevice request, CancellationToken cancellationToken)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var account = _db.Accounts.First(x => x.UserName == userName);

            await _db
                .Devices
                .AddAsync(new Domain.Device()
                {
                    Id = request.Id,
                    Enabled = request.Enabled
                });

            await _db
                .AccountDevices
                .AddAsync(new Domain.AccountDevice
                {
                    DeviceId = request.Id,
                    AccountId = account.Id
                });

            await _hiveMqCredentialsService.AddCredentials(request.Id, request.Password);

            await _db.SaveChangesAsync();

            if (request.Enabled)
            {
                await _mqttService.SubscribeDevice(request.Id);
            }
        }
    }
}
