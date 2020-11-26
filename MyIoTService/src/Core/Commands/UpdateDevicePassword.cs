using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Commands
{
    public class UpdateDevicePassword : IRequest
    {
        public string Id { get; set; }
        public string Password { get; set; }
    }
}
