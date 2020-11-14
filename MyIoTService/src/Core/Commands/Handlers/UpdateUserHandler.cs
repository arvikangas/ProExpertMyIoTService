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
    public class UpdateUserHandler : AsyncRequestHandler<UpdateUser>
    {
        private readonly MyIoTDbContext _db;

        public UpdateUserHandler(MyIoTDbContext db)
        {
            _db = db;
        }

        protected async override Task Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FindAsync(request.Id);
            user.Name = request.Name;
            await _db.SaveChangesAsync();
        }
    }
}
