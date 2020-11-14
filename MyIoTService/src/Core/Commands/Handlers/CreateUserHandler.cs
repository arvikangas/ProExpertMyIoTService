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
    public class CreateUserHandler : AsyncRequestHandler<CreateUser>
    {
        private readonly MyIoTDbContext _db;

        public CreateUserHandler(MyIoTDbContext db)
        {
            _db = db;
        }

        protected async override Task Handle(CreateUser request, CancellationToken cancellationToken)
        {
            await _db
                .Users
                .AddAsync(new Domain.User()
                {
                    Id = request.Id,
                    Name = request.Name
                });
            await _db.SaveChangesAsync();
        }
    }
}
