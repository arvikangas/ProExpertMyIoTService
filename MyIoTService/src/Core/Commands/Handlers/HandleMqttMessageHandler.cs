using MediatR;
using Microsoft.Extensions.Logging;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Services.Mqtt;
using MyIoTService.Domain;
using MyIoTService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Commands.Handlers
{
    public class HandleMqttMessageHandler : AsyncRequestHandler<HandleMqttMessage>
    {
        private readonly MyIoTDbContext _db;
        private readonly ILogger<HandleMqttMessageHandler> _logger;

        public HandleMqttMessageHandler(
            MyIoTDbContext db,
            ILogger<HandleMqttMessageHandler> logger)
        {
            _db = db;
            _logger = logger;
        }

        protected async override Task Handle(HandleMqttMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling mqtt message. Topic {Topic}, Payload {Payload}", request.Topic, request.Payload);
            var topics = request.Topic.Split('/');

            // Topic format is devices/{deviceId}/{dataType}
            if (topics.Length != 3)
            {
                _logger.LogInformation("Unsupported topic format. {Topic}", request.Topic);
                return;

            }

            var deviceId = topics[1];
            var dataTypeString = topics[2];

            int dataType;
            if (!int.TryParse(dataTypeString, out dataType))
            {
                _logger.LogInformation("Invalid format for data type {DataType}", dataTypeString);
                return;
            }

            if (!AllowedCodes.Contains(dataType))
            {
                _logger.LogInformation("Unknown data type {DataType}", dataType);
                return;
            }

            string payloadString = UTF8Encoding.UTF8.GetString(request.Payload);
            int payLoad;
            if(string.IsNullOrEmpty(payloadString) || !int.TryParse(payloadString, out payLoad))
            {
                _logger.LogInformation("Invalid payload format. Payload: {Payload}", payloadString);
                return;
            }

            var limits = CodeMinMax[dataType];
            if(payLoad < limits.min || payLoad > limits.max)
            {
                _logger.LogInformation("Payload out of bounds. Payload: {Payload}, min: {Min}, max: {max}", payLoad, limits.min, limits.max);
                return;
            }

            var device = _db.Devices.Find(deviceId);
            if (device is null)
            {
                _logger.LogInformation("No device with id {Id}", deviceId);
                return;
            }

            var entry = new DeviceDataIncoming
            {
                DeviceId = device.Id,
                DataType = dataType,
                TimeStamp = DateTime.Now,
                Value = payLoad
            };

            _db.DeviceDataIncoming.Add(entry);

            switch(payLoad)
            {
                case 1000: device.InsideTemperature = payLoad; break;
                case 1001: device.OutsideTemperature = payLoad; break;
                case 1010: device.HasOutsideTemperatureSensor = payLoad == 0 ? false : true; break;
                case 1011: device.OutsideTemperature = payLoad; break;
                case 1020: device.OutsideTemperature = payLoad; break;
                case 2000: device.OperationTimeInSec = payLoad; break;
                case 2010: device.WorkingHour = payLoad; break;
                case 4040: device.IsOperational = payLoad == 0 ? false : true; break;
                case 4050: device.SilentMode = payLoad == 0 ? false : true; break;
                case 8000: device.MachineIsBroken = payLoad == 0 ? false : true; break;
                case 9000: device.SerialNumber = payLoad; break;
                default: break;
            }
            await _db.SaveChangesAsync();
        }

        static HashSet<int> AllowedCodes = new HashSet<int>
        {
            1000,
            1001,
            1010,
            1011,
            1020,
            2000,
            2010,
            4040,
            4050,
            8000,
            9000
        };

        static Dictionary<int, (int min, int max)> CodeMinMax = new Dictionary<int, (int, int)>
        {
            { 1000, (-150, 150) },
            { 1001, (-100, 100) },
            { 1010, (-0, 1) },
            { 1011, (-100, 100) },
            { 1020, (-100, 100) },
            { 2000, ( 0, int.MaxValue) },
            { 2010, ( 0, int.MaxValue) },
            { 4040, (0, 1) },
            { 4050, (0, 1) },
            { 8000, (0, 1) },
            { 9000, (0, 999999) },
        };
    }
}
