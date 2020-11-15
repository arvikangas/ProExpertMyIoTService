using MediatR;
using MyIoTService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Queries
{
    public class GetDeviceDataIncomingHistory : IRequest<IEnumerable<DeviceDataDto>>
    {
        public string Id { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
