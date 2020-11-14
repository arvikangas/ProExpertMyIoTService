using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Commands
{
    public class UpdateDevice : IRequest
    {
        public string Id { get; set; }
        public bool Enabled { get; set; }
    }
}
