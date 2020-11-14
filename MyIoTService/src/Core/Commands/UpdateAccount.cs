using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Commands
{
    public class UpdateAccount : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
