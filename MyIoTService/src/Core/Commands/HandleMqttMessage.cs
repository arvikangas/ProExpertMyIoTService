using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Commands
{
    public class HandleMqttMessage : IRequest
    {
        public string Topic { get; set; }
        public byte[] Payload { get; set; }
    }
}
