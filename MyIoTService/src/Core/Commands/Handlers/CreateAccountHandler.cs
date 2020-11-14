using MediatR;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Dtos;
using MyIoTService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Commands.Handlers
{
    public class CreateAccountHandler : AsyncRequestHandler<CreateAccount>
    {
        private readonly MyIoTDbContext _db;

        public CreateAccountHandler(MyIoTDbContext db)
        {
            _db = db;
        }

        protected async override Task Handle(CreateAccount request, CancellationToken cancellationToken)
        {
            await _db
                .Accounts
                .AddAsync(new Domain.Account()
                {
                    Id = request.Id,
                    Name = request.Name
                });
            await _db.SaveChangesAsync();
        }
    }
}
