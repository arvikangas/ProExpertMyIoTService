using MediatR;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Queries.Handlers
{
    public class GetDeviceHandler : IRequestHandler<GetDevice, DeviceDto>
    {
        private readonly IDeviceRepository _deviceRepository;

        public GetDeviceHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<DeviceDto> Handle(GetDevice request, CancellationToken cancellationToken)
        {
            var result = await _deviceRepository.Get(request.Id);
            return result.ToDto();
        }
    }
}
