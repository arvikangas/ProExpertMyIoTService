using MediatR;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Queries.Handlers
{
    public class GetDeviceDataIncomingHistoryHandler : IRequestHandler<GetDeviceDataIncomingHistory, IEnumerable<DeviceDataDto>>
    {
        private readonly IDeviceDataIncomingRepository _deviceDataIncomingRepository;

        public GetDeviceDataIncomingHistoryHandler(IDeviceDataIncomingRepository deviceDataIncomingRepository)
        {
            _deviceDataIncomingRepository = deviceDataIncomingRepository;
        }

        public async Task<IEnumerable<DeviceDataDto>> Handle(GetDeviceDataIncomingHistory request, CancellationToken cancellationToken)
        {
            var result = await _deviceDataIncomingRepository.Get(request.Id, request.From, request.To);
            var dtos = result.Select(x => x.ToDto());

            return dtos;

        }
    }
}
