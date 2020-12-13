using MediatR;
using Microsoft.AspNetCore.Http;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Repositories;
using MyIoTService.Core.Services.Mqtt;
using MyIoTService.Domain;
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
        private readonly IAccountRepository _accountRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IAccountDeviceRepository _accountDeviceRepository;
        private readonly IMqttService _mqttService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHiveMqCredentialsService _hiveMqCredentialsService;

        public CreateDeviceHandler(
            IAccountRepository accountRepository,
            IDeviceRepository deviceRepository,
            IAccountDeviceRepository accountDeviceRepository,
            IMqttService mqttService,
            IHttpContextAccessor httpContextAccessor,
            IHiveMqCredentialsService hiveMqCredentialsService)
        {
            _accountRepository = accountRepository;
            _deviceRepository = deviceRepository;
            _accountDeviceRepository = accountDeviceRepository;
            _mqttService = mqttService;
            _httpContextAccessor = httpContextAccessor;
            _hiveMqCredentialsService = hiveMqCredentialsService;
        }

        protected async override Task Handle(CreateDevice request, CancellationToken cancellationToken)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var account = await _accountRepository.GetByUserName(userName);

            await _deviceRepository.Create(new Domain.Device()
            {
                Id = request.Id,
                Enabled = request.Enabled
            });

            await _accountDeviceRepository.Create(new Domain.AccountDevice
            {
                DeviceId = request.Id,
                AccountId = account.Id
            });

            await _hiveMqCredentialsService.AddCredentials(request.Id, request.Password);

            if (request.Enabled)
            {
                await _mqttService.SubscribeDevice(request.Id);
            }
        }
    }
}
