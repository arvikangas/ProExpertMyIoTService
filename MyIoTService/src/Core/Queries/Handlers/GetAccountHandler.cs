using MediatR;
using MyIoTService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Queries.Handlers
{
    public class CreateUserHandler : IRequestHandler<GetAccount, AccountDto>
    {
        public CreateUserHandler()
        {

        }

        public async Task<AccountDto> Handle(GetAccount request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
