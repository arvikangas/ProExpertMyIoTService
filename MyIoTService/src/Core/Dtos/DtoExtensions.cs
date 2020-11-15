using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Dtos
{
    public static class DtoExtensions
    {
        public static DeviceDto ToDto(this Device device)
        {
            return new DeviceDto
            {
                Id = device.Id,
                Enabled = device.Enabled,
                HasOutsideTemperatureSensor = device.HasOutsideTemperatureSensor,
                InsideTemperature = device.InsideTemperature,
                IsOperational = device.IsOperational,
                MachineIsBroken = device.MachineIsBroken,
                OperationTimeInSec = device.OperationTimeInSec,
                OutsideTemperature = device.OutsideTemperature,
                SerialNumber = device.SerialNumber,
                SilentMode = device.SilentMode,
                WaterTemperature = device.WaterTemperature,
                WorkingHour = device.WorkingHour
            };
        }

        public static DeviceDataDto ToDto(this DeviceDataIncoming data)
        {
            return new DeviceDataDto
            {
                DataType = data.DataType,
                TimeStamp = data.TimeStamp,
                Value = data.Value
            };
        }
    }
}
