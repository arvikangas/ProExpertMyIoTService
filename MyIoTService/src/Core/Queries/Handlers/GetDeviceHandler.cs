using MediatR;
using Microsoft.AspNetCore.Http;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Queries.Handlers
{
    public class GetDeviceHandler : IRequestHandler<GetDevice, DeviceDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountDeviceRepository _accountDeviceRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetDeviceHandler(
            IAccountRepository accountRepository,
            IAccountDeviceRepository accountDeviceRepository,
            IDeviceRepository deviceRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _accountDeviceRepository = accountDeviceRepository;
            _deviceRepository = deviceRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DeviceDto> Handle(GetDevice request, CancellationToken cancellationToken)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var account = await _accountRepository.GetByUserName(userName);
            var accountDevice = await _accountDeviceRepository.Get((account.Id, request.Id));
            if(accountDevice is null)
            {
                return null;
            }
            var result = await _deviceRepository.Get(request.Id);
            return result.ToDto();
        }
    }
}
