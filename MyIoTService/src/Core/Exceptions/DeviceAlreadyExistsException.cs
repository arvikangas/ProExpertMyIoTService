using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Exceptions
{
    public class DeviceAlreadyExistsException : Exception
    {
        public DeviceAlreadyExistsException(string deviceId) : base($"Device {deviceId} already exists")
        {

        }
    }
}
