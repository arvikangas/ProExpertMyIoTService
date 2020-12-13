using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIoTService.Core.Repositories
{
    public interface IDeviceDataOutgoingRepository : IRepository<DeviceDataOutgoing, (string, DateTime, int)>
    {
        Task<IEnumerable<DeviceDataOutgoing>> Get(string deviceId, DateTime? from, DateTime? to);
    }
}
