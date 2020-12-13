using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class GetDeviceDataOutgoingHistoryHandler : IRequestHandler<GetDeviceDataOutgoingHistory, IEnumerable<DeviceDataDto>>
    {
        private readonly IDeviceDataOutgoingRepository _deviceDataOutgoingRepository;

        public GetDeviceDataOutgoingHistoryHandler(IDeviceDataOutgoingRepository deviceDataOutgoingRepository)
        {
            _deviceDataOutgoingRepository = deviceDataOutgoingRepository;
        }

        public async Task<IEnumerable<DeviceDataDto>> Handle(GetDeviceDataOutgoingHistory request, CancellationToken cancellationToken)
        {
            var result = await _deviceDataOutgoingRepository.Get(request.Id, request.From, request.To);
            var dtos = result.Select(x => x.ToDto());

            return dtos;
        }
    }
}
