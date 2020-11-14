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
    public class UpdateAccountHandler : AsyncRequestHandler<UpdateAccount>
    {
        private readonly MyIoTDbContext _db;

        public UpdateAccountHandler(MyIoTDbContext db)
        {
            _db = db;
        }

        protected async override Task Handle(UpdateAccount request, CancellationToken cancellationToken)
        {
            var user = await _db.Accounts.FindAsync(request.Id);
            user.Name = request.Name;
            await _db.SaveChangesAsync();
        }
    }
}
