using Auth.Application.Provinces.CrawlProvinces;
using BuildingBlocks;

namespace Auth.API.Controllers.Provinces.CrawlProvinces
{
    [ApiController]
    [Route(NameRouter.PROVINCES_ROUTER)]
    public class CrawlProvincesEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(NameRouter.CRAWL_PROVINCES)]
        public async Task<IActionResult> CrawlProvinces()
        {
            return Ok(await mediator.Send(new CrawlProvincesRequest()));
        }
    }
}
