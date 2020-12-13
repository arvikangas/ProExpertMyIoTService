using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Domain
{
    public class DeviceDataOutgoing : IEntity<(string, DateTime, int)>
    {
        public string DeviceId { get; set; }
        public Device Device { get; set; }

        public DateTime TimeStamp { get; set; }
        public int DataType { get; set; }
        public int? Value { get; set; }
        public (string, DateTime, int) Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
