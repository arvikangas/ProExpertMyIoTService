using MediatR;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Services.Mqtt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Commands.Handlers
{
    public class UpdateDevicePasswordHandler : AsyncRequestHandler<UpdateDevicePassword>
    {
        private readonly IHiveMqCredentialsService _hiveMqCredentialsService;

        public UpdateDevicePasswordHandler(IHiveMqCredentialsService hiveMqCredentialsService)
        {
            _hiveMqCredentialsService = hiveMqCredentialsService;
        }

        protected async override Task Handle(UpdateDevicePassword request, CancellationToken cancellationToken)
        {
            await _hiveMqCredentialsService.EditCredentials(request.Id, request.Password);
        }
    }
}
