using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientEmulator.Services
{
    public class DeviceState
    {
        public string DeviceId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int InsideTemperature { get; set; }
        public int OutsideTemperature { get; set; }
        public bool HasOutsideTemperatureSensor { get; set; }
        public int WaterTemperature { get; set; }
        public int OperationTimeInSec { get; set; } = 0;
        public int WorkingHour { get; set; } = 0;
        public bool IsOperational { get; set; }
        public bool SilentMode { get; set; }
        public bool MachineIsBroken { get; set; }
        public int SerialNumber { get; set; }
    }
}
