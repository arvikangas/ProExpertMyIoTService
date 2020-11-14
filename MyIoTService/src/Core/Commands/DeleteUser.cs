using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Commands
{
    public class DeleteUser : IRequest
    {
        public Guid Id { get; set; }
    }
}
