using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Commands
{
    public class DeleteDevice : IRequest
    {
        public string Id { get; set; }
    }
}
