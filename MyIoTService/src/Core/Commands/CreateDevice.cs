using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Commands
{
    public class CreateDevice : IRequest
    {
        public string Id { get; set; }
        public bool Enabled { get; set; }

        public Guid AccountId { get; set; }
    }
}
