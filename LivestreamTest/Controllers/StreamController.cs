using LivestreamTest.Logic;
using Microsoft.AspNetCore.Mvc;

namespace LivestreamTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StreamController : ControllerBase
    {
        private readonly CloudflareStreamService _streamService;

        public StreamController(CloudflareStreamService streamService)
        {
            _streamService = streamService;
        }

        [HttpPost("create-live-input")]
        public async Task<IActionResult> CreateLiveInput([FromBody] string name)
        {
            var result = await _streamService.CreateLiveInput(name);
            return Ok(result);
        }
    }
}