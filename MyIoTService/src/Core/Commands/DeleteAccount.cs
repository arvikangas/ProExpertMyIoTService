using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Commands
{
    public class DeleteAccount : IRequest
    {
        public Guid Id { get; set; }
    }
}
