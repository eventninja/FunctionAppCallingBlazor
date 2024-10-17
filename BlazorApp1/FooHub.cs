using Microsoft.AspNetCore.SignalR;

namespace BlazorApp1
{
    public class FooHub : Hub
    {
        public const string HubUrl = "/foohub";

        public async Task Broadcast(string message)
        {
            await Clients.All.SendAsync("Broadcast", message);
        }
    }
}
