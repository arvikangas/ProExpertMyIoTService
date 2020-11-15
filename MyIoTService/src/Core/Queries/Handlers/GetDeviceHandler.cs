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
            var result = await _db.Devices.FindAsync(request.Id);
            return result.ToDto();
        }
    }
}
