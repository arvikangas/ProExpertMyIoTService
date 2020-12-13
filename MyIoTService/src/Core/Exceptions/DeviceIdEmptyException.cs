using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Exceptions
{
    public class DeviceIdEmptyException : Exception
    {
        public DeviceIdEmptyException() : base($"Device id is empty.")
        {

        }
    }
}
