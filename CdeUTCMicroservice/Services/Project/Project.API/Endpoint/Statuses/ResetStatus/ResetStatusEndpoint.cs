﻿using Project.Application.Features.Statuses.ResetStatus;

namespace Project.API.Endpoint.Statuses.ResetStatus
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class ResetStatusEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("{projectId}/" + NameRouter.STATUS)]
        public async Task<IActionResult> ResetStatus(int projectId)
        {
            return Ok(await mediator.Send(new ResetStatusRequest() { ProjectId = projectId }));
        }
    }
}
