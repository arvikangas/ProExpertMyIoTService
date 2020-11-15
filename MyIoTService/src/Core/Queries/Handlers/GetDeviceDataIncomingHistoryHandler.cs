﻿using MediatR;
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
    public class GetDeviceDataIncomingHistoryHandler : IRequestHandler<GetDeviceDataIncomingHistory, IEnumerable<DeviceDataDto>>
    {
        private readonly MyIoTDbContext _db;

        public GetDeviceDataIncomingHistoryHandler(MyIoTDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<DeviceDataDto>> Handle(GetDeviceDataIncomingHistory request, CancellationToken cancellationToken)
        {
            var query = _db
                .DeviceDataIncoming
                .AsQueryable();

            query = query.Where(x => x.DeviceId == request.Id);

            if(request.From is { })
            {
                query = query.Where(x => x.TimeStamp >= request.From.Value);
            }

            if (request.To is { })
            {
                query = query.Where(x => x.TimeStamp <= request.To.Value);
            }

            var result = await query.Select(x => x.ToDto()).ToListAsync();

            return result;

        }
    }
}
