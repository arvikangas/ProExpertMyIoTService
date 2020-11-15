using MediatR;
using MyIoTService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Queries
{
    public class GetDevices : IRequest<IEnumerable<DeviceDto>>
    {
    }
}
