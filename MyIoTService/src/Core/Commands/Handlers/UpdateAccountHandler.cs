using MediatR;
using Microsoft.EntityFrameworkCore;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Commands.Handlers
{
    public class UpdateAccountHandler : AsyncRequestHandler<UpdateAccount>
    {
        private readonly IAccountRepository _accountRepository;

        public UpdateAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        protected async override Task Handle(UpdateAccount request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.Get(request.Id);
            user.UserName = request.Name;
            await _accountRepository.Update(user);
        }
    }
}
