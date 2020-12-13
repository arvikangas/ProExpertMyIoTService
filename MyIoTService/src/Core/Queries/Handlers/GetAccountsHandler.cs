using MediatR;
using Microsoft.EntityFrameworkCore;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Queries.Handlers
{
    public class UpdateUserHandler : IRequestHandler<GetAccounts, IEnumerable<AccountDto>>
    {
        private readonly IAccountRepository _accountRepository;

        public UpdateUserHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<AccountDto>> Handle(GetAccounts request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.GetAll();
            var dtos = result.Select(x => new AccountDto
            {
                Id = x.Id,
                Name = x.UserName
            });

            return dtos;
        }
    }
}
