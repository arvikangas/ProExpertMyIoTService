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
    public class DeleteAccountHandler : AsyncRequestHandler<DeleteAccount>
    {
        private readonly MyIoTDbContext _db;

        public DeleteAccountHandler(MyIoTDbContext db)
        {
            _db = db;
        }

        protected async override Task Handle(DeleteAccount request, CancellationToken cancellationToken)
        {
            var user = await _db.Accounts.FindAsync(request.Id);
            _db.Accounts.Remove(user);
            await _db.SaveChangesAsync();
        }
    }
}
