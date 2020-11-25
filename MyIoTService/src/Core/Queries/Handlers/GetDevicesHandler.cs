using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyIoTService.Core.Dtos;
using MyIoTService.Infrastructure.EF;
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
        private readonly MyIoTDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetDevicesHandler(MyIoTDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<DeviceDto>> Handle(GetDevices request, CancellationToken cancellationToken)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var account = _db.Accounts.First(x => x.UserName == userName);
            var devices = await _db
                .AccountDevices
                .Where(x => x.AccountId == account.Id)
                .Select(x => x.Device)
                .Select(x => x.ToDto())
                .ToListAsync();

            return devices;
        }
    }
}
