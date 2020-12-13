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
    public class DeviceRepository : Repository<Device, string>, IDeviceRepository
    {
        public DeviceRepository(MyIoTDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Device>> FindAllEnabled()
        {
            return await _dbSet
                .Where(x => x.Enabled)
                .ToListAsync();
        }

        public async Task<Account> GetByUserName(string userName)
        {
            return await _db.Accounts.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<IEnumerable<Device>> GetForAccount(Guid accountId)
        {
            return await _db
                .AccountDevices
                .Where(x => x.AccountId == accountId)
                .Select(x => x.Device)
                .ToListAsync();
        }
    }
}
