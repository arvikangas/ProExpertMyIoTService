using MediatR;
using MyIoTService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Queries
{
    public class GetAccount : IRequest<AccountDto>
    {
        public Guid Id { get; set; }
    }
}
