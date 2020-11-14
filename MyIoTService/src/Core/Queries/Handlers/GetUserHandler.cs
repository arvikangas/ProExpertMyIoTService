using MediatR;
using MyIoTService.Core.Dtos;
using MyIoTService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Queries.Handlers
{
    public class CreateUserHandler : IRequestHandler<GetUser, UserDto>
    {
        public CreateUserHandler(MyIoTDbContext db)
        {

        }

        public async Task<UserDto> Handle(GetUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
