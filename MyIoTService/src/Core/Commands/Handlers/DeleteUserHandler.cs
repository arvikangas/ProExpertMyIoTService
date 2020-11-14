using MediatR;
using Microsoft.EntityFrameworkCore;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Dtos;
using MyIoTService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Commands.Handlers
{
    public class DeleteUserHandler : AsyncRequestHandler<DeleteUser>
    {
        private readonly MyIoTDbContext _db;

        public DeleteUserHandler(MyIoTDbContext db)
        {
            _db = db;
        }

        protected async override Task Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FindAsync(request.Id);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }
}
