using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Repositories
{
    public interface IAccountDeviceRepository : IRepository<AccountDevice, (Guid, string)>
    {
    }
}
