using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Dtos
{
    public class DeviceDataDto
    {
        public DateTime TimeStamp { get; set; }
        public int DataType { get; set; }
        public int? Value { get; set; }
    }
}
