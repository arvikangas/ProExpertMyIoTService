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
    [Route("devices")]
    public class DevicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DevicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _mediator.Send(new GetDevices());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var result = await _mediator.Send(new GetDevice() { Id = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute]string id, [FromBody]UpdateDevice command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost()]
        public async Task<ActionResult> Post(CreateDevice command)
        {
            await _mediator.Send(command);
            return Created($"/devices/{command.Id}", null);
        }

    }
}
