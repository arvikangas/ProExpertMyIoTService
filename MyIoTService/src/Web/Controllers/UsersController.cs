using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Queries;

namespace MyIoTService.Web.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _mediator.Send(new GetUsers());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetUser() { Id = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute]Guid id, [FromBody]UpdateUser command)
        {
            command.Id = id;
            await _mediator.Send(new GetUser() { Id = id });
            return NoContent();
        }

        [HttpPost()]
        public async Task<ActionResult> Post(CreateUser command)
        {
            command.Id = Guid.NewGuid();
            await _mediator.Send(command);
            return Created($"/users/{command.Id}", null);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteUser { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }


    }
}
