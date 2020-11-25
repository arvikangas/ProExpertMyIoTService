using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Queries;

namespace MyIoTService.Web.Controllers
{
    [ApiController]
    [Route("accounts")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _mediator.Send(new GetAccounts());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetAccount() { Id = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute]Guid id, [FromBody]UpdateAccount command)
        {
            command.Id = id;
            await _mediator.Send(new GetAccount() { Id = id });
            return NoContent();
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<ActionResult> Post(CreateAccount command)
        {
            command.Id = Guid.NewGuid();
            await _mediator.Send(command);
            return Created($"/users/{command.Id}", null);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> SignIn(SignIn command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteAccount { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }


    }
}
