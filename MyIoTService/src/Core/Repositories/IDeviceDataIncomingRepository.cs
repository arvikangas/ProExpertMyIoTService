using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIoTService.Core.Repositories
{
    public interface IDeviceDataIncomingRepository : IRepository<DeviceDataIncoming, (string, DateTime, int)>
    {
        Task<IEnumerable<DeviceDataIncoming>> Get(string deviceId, DateTime? from, DateTime? to);
    }
}
