using MediatR;
using MyIoTService.Core.Dtos;
using MyIoTService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Queries.Handlers
{
    public class GetDeviceHandler : IRequestHandler<GetDevice, DeviceDto>
    {
        private readonly MyIoTDbContext _db;

        public GetDeviceHandler(MyIoTDbContext db)
        {
            _db = db;
        }

        public async Task<DeviceDto> Handle(GetDevice request, CancellationToken cancellationToken)
        {
            var result = _db.Devices.Find(request.Id);
            return new DeviceDto
            {
                Id = result.Id,
                Enabled = result.Enabled
            };
        }
    }
}
