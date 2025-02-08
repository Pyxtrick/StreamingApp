using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StreamingApp.API.SignalRHub;
using StreamingApp.Domain.Enums;
using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpPost]
    public async void TestLogic([FromServices] IHubContext<ChatHub> clientHub)
    {
        ChatDto chatMessage = new("1", "testuser", "#fff", null, "hello", "", null, new() { new("kekw", "assets/3x.webp") }, ChatOriginEnum.Twtich, ChatDisplayEnum.allChat,
            new() { AuthEnum.undefined }, new() { SpecialMessgeEnum.Undefined } , EffectEnum.none, DateTime.Now);

        //MessageDto messageDto = new(e.ChatMessage.Id, false, channel, userId, userName, colorHex, replayMessage, message, emoteReplacedMessage, pointRediam, bits, emoteSet, badges, ChatOriginEnum.Twtich, auths, specialMessage, EffectEnum.none, e.ChatMessage.IsSubscriber, e.ChatMessage.SubscribedMonthCount, DateTime.UtcNow);

        Console.WriteLine($"message {chatMessage.UserName}");

        await clientHub.Clients.All.SendAsync("ReceiveChatMessage", chatMessage);
    }
}
