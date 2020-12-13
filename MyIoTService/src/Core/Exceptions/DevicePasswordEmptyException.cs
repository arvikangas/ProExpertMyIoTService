using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Exceptions
{
    public class DevicePasswordEmptyException : Exception
    {
        public DevicePasswordEmptyException() : base($"Device password is empty.")
        {

        }
    }
}
