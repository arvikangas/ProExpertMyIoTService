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

        // Device data
        public bool? HasOutsideTemperatureSensor { get; set; }
        public int? OperationTimeInSec { get; set; }
        public int? WorkingHour { get; set; }
        public bool? IsOperational { get; set; }
        public bool? SilentMode { get; set; }
        public int? SerialNumber { get; set; }
    }
}
