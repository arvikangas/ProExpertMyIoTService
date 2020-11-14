using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Domain
{
    public class AccountDevice
    {
        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public string DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
