using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Queries.Handlers
{
    public class GetDevicesHandler : IRequestHandler<GetDevices, IEnumerable<DeviceDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetDevicesHandler(
            IAccountRepository accountRepository,
            IDeviceRepository deviceRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _deviceRepository = deviceRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<DeviceDto>> Handle(GetDevices request, CancellationToken cancellationToken)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var account = await _accountRepository.GetByUserName(userName);
            var devices = await _deviceRepository.GetForAccount(account.Id);
            var dtos = devices.Select(x => x.ToDto());

            return dtos;
        }
    }
}
