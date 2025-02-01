using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StreamingApp.API.SignalRHub;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpPost]
    public async void TestLogic([FromServices] IHubContext<ChatHub> clientHub)
    {
        // Send Messages to the ReceiveMessage
        await clientHub.Clients.All.SendAsync("ReceiveMessage", "test");

        //await clientHub.SendMessage(new ChatDto("1", "testuser", "", null, "hello", "", null, new() { new("1", "test")}, ChatOriginEnum.Twtich, ChatDisplayEnum.allChat, new() { AuthEnum.undefined}, null, EffectEnum.none, DateTime.UtcNow));
    }
}
