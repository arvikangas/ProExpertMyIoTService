using ClientEmulator.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientEmulator.Services
{
    public class DeviceState
    {
        private readonly IHubContext<MqttHub> _hub;

        public DeviceState(IHubContext<MqttHub> hub)
        {
            _hub = hub;
        }

        public string DeviceId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        private int _insideTemperature;
        public int InsideTemperature 
        {
            get => _insideTemperature; 
            set 
            {
                _insideTemperature = value;
                _hub.Clients.All.SendAsync("State", "1000", _insideTemperature.ToString()); 
            } 
        }

        private int _outsideTemperature;
        public int OutsideTemperature
        {
            get => _outsideTemperature;
            set
            {
                _outsideTemperature = value;
                _hub.Clients.All.SendAsync("State", "1001", _outsideTemperature.ToString());
            }
        }

        private bool _hasOutsideTemperatureSensor;
        public bool HasOutsideTemperatureSensor
        {
            get => _hasOutsideTemperatureSensor;
            set
            {
                _hasOutsideTemperatureSensor = value;
                _hub.Clients.All.SendAsync("State", "1010", _hasOutsideTemperatureSensor.ToString());
            }
        }

        private int _waterTemperature;
        public int WaterTemperature
        {
            get => _waterTemperature;
            set
            {
                _waterTemperature = value;
                _hub.Clients.All.SendAsync("State", "1020", _waterTemperature.ToString());
            }
        }

        private int _operationTimeInSec;
        public int OperationTimeInSec
        {
            get => _operationTimeInSec;
            set
            {
                _operationTimeInSec = value;
                _hub.Clients.All.SendAsync("State", "2000", _operationTimeInSec.ToString());
            }
        }

        private int _workingHour;
        public int WorkingHour
        {
            get => _workingHour;
            set
            {
                _workingHour = value;
                _hub.Clients.All.SendAsync("State", "2010", _workingHour.ToString());
            }
        }

        private bool _isOperational;
        public bool IsOperational
        {
            get => _isOperational;
            set
            {
                _isOperational = value;
                _hub.Clients.All.SendAsync("State", "4040", _isOperational.ToString());
            }
        }

        private bool _silentMode;
        public bool SilentMode
        {
            get => _silentMode;
            set
            {
                _silentMode = value;
                _hub.Clients.All.SendAsync("State", "4050", _silentMode.ToString());
            }
        }

        private bool _machineIsBroken;
        public bool MachineIsBroken
        {
            get => _machineIsBroken;
            set
            {
                _machineIsBroken = value;
                _hub.Clients.All.SendAsync("State", "8000", _machineIsBroken.ToString());
            }
        }

        private int _serialNumber;
        public int SerialNumber
        {
            get => _serialNumber;
            set
            {
                _serialNumber = value;
                _hub.Clients.All.SendAsync("State", "9000", _serialNumber.ToString());
            }
        }

        public void SetProperty(string command, int value)
        {
            switch (command)
            {
                case "1000":
                    InsideTemperature = value; break;
                case "1001":
                    OutsideTemperature = value; break;
                case "1010":
                    HasOutsideTemperatureSensor = value == 1 ? true : false; break;
                case "1020":
                    WaterTemperature = value; break;
                case "2000":
                    OperationTimeInSec = value; break;
                case "2010":
                    WorkingHour = value; break;
                case "4040":
                    IsOperational = value == 1 ? true : false; break;
                case "4050":
                    SilentMode = value == 1 ? true : false; break;
                case "8000":
                    MachineIsBroken = value == 1 ? true : false; break;
                case "9000":
                    SerialNumber = value; break;
                default: break;
            }
        }
    }
}
