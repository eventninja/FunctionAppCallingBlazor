using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp1.Controllers
{
    [Route("api/broadcast")]
    [ApiController]
    public class BroadcastController : ControllerBase
    {
        private readonly IHubContext<FooHub> _hub;

        public BroadcastController(IHubContext<FooHub> hub)
        {
            _hub = hub;
        }

        [HttpGet]
        public async Task Get(string message)
        {
            await _hub.Clients.All.SendAsync("Broadcast", message);
        }
    }
}
