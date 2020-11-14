using MediatR;
using Microsoft.EntityFrameworkCore;
using MyIoTService.Core.Dtos;
using MyIoTService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Queries.Handlers
{
    public class UpdateUserHandler : IRequestHandler<GetUsers, IEnumerable<UserDto>>
    {
        private readonly MyIoTDbContext _db;

        public UpdateUserHandler(MyIoTDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsers request, CancellationToken cancellationToken)
        {
            var result = await _db.Users
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            return result;
        }
    }
}
