using MediatR;
using Microsoft.EntityFrameworkCore;
using MyIoTService.Core.Dtos;
using MyIoTService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Queries.Handlers
{
    public class GetDevicesHandler : IRequestHandler<GetDevices, IEnumerable<DeviceDto>>
    {
        private readonly MyIoTDbContext _db;

        public GetDevicesHandler(MyIoTDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<DeviceDto>> Handle(GetDevices request, CancellationToken cancellationToken)
        {
            var devices = await _db
                .Devices
                .Select(x => new DeviceDto
                {
                    Id = x.Id,
                    Enabled = x.Enabled
                })
                .ToListAsync();

            return devices;
        }
    }
}
