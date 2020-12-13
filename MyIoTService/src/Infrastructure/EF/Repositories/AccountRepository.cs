using Microsoft.EntityFrameworkCore;
using MyIoTService.Core.Repositories;
using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIoTService.Infrastructure.EF.Repositories
{
    public class AccountRepository : Repository<Account, Guid>, IAccountRepository
    {
        public AccountRepository(MyIoTDbContext db) : base(db)
        {
        }

        public async Task<Account> GetByUserName(string userName)
        {
            return await _db.Accounts.FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
