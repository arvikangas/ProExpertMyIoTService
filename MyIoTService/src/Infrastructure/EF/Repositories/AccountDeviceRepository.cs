using Microsoft.EntityFrameworkCore;
using MyIoTService.Core.Repositories;
using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIoTService.Infrastructure.EF.Repositories
{
    public class AccountDeviceRepository : Repository<AccountDevice, (Guid, string)>, IAccountDeviceRepository
    {
        public AccountDeviceRepository(MyIoTDbContext db) : base(db)
        {
        }
    }
}
