using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Domain
{
    public class Device
    {
        public string Id { get; set; }
        public bool Enabled { get; set; }


        // Device data
        public int InsideTemperature { get; set; }
        public int OutsideTemperature { get; set; }
        public bool HasOutsideTemperatureSensor { get; set; }
        public int WaterTemperature { get; set; }
        public int OperationTimeInSec { get; set; }
        public int WorkingHour { get; set; }
        public bool IsOperational { get; set; }
        public bool SilentMode { get; set; }
        public bool MachineIsBroken { get; set; }
        public int SerialNumber { get; set; }
    }
}
