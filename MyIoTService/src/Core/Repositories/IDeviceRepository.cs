using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIoTService.Core.Repositories
{
    public interface IDeviceRepository : IRepository<Device, string>
    {
        Task<IEnumerable<Device>> FindAllEnabled();
        Task<IEnumerable<Device>> GetForAccount(Guid accountId);
    }
}
