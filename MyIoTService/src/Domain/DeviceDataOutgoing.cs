using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Domain
{
    public class DeviceDataOutgoing
    {
        public string DeviceId { get; set; }
        public Device Device { get; set; }

        public DateTime TimeStamp { get; set; }

        public int DataTypeId { get; set; }
        public DataType DataType { get; set; }

        public int Value { get; set; }
    }
}
