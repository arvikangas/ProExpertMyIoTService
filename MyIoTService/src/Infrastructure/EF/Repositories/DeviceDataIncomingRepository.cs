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
    public class DeviceDataIncomingRepository : Repository<DeviceDataIncoming, (string, DateTime, int)>, IDeviceDataIncomingRepository
    {
        public DeviceDataIncomingRepository(MyIoTDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<DeviceDataIncoming>> Get(string deviceId, DateTime? from, DateTime? to)
        {
            var query = _dbSet.AsQueryable();

            query = query.Where(x => x.DeviceId == deviceId);

            if(from is { })
            {
                query = query.Where(x => x.TimeStamp >= from.Value);
            }

            if (to is { })
            {
                query = query.Where(x => x.TimeStamp <= to.Value);
            }

            return await query.ToListAsync();
        }

        public async override Task<DeviceDataIncoming> Get((string, DateTime, int) id)
        {
            return await _dbSet.FindAsync(id.Item1, id.Item2, id.Item3);
        }
    }
}
