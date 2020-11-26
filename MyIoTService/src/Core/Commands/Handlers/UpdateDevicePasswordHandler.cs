using MediatR;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Services.Mqtt;
using MyIoTService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Commands.Handlers
{
    public class UpdateDevicePasswordHandler : AsyncRequestHandler<UpdateDevicePassword>
    {
        private readonly MyIoTDbContext _db;
        private readonly IHiveMqCredentialsService _hiveMqCredentialsService;

        public UpdateDevicePasswordHandler(MyIoTDbContext db, IHiveMqCredentialsService hiveMqCredentialsService)
        {
            _db = db;
            _hiveMqCredentialsService = hiveMqCredentialsService;
        }

        protected async override Task Handle(UpdateDevicePassword request, CancellationToken cancellationToken)
        {
            await _hiveMqCredentialsService.EditCredentials(request.Id, request.Password);
        }
    }
}
