using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIoTService.Core.Repositories
{
    public interface IAccountRepository : IRepository<Account, Guid>
    {
        Task<Account> GetByUserName(string userName);
    }
}
