using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StreamingApp.API.SignalRHub;
using StreamingApp.Domain.Enums;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.API.Utility.Caching.Interface;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpPost("sendChatDto")]
    public async void TestLogic([FromServices] IHubContext<ChatHub> clientHub)
    {
        int t = new Random().Next(1, 50);

        Random random = new Random();

        Console.WriteLine($"messageId {t}");

        string mess = "hello test";
         const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        mess = new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());

        ChatDto chatMessage = new(t.ToString(), "testuser", "#fff", null, mess, "", null, new() { new("kekw", "assets/3x.webp") }, ChatOriginEnum.Twtich, ChatDisplayEnum.allChat,
            new() { AuthEnum.Undefined }, new() { SpecialMessgeEnum.Undefined } , EffectEnum.none, DateTime.Now);

        //MessageDto messageDto = new(e.ChatMessage.Id, false, channel, userId, userName, colorHex, replayMessage, message, emoteReplacedMessage, pointRediam, bits, emoteSet, badges, ChatOriginEnum.Twtich, auths, specialMessage, EffectEnum.none, e.ChatMessage.IsSubscriber, e.ChatMessage.SubscribedMonthCount, DateTime.UtcNow);

        Console.WriteLine($"message {chatMessage.UserName}");

        await clientHub.Clients.All.SendAsync("ReceiveChatMessage", chatMessage);
    }

    [HttpPut("AddTochache")]
    public async void AddDataToCache([FromServices] ITwitchCallCache _twitchCallCache)
    {
        MessageDto message = new("1", false, "testuser", "1", "testuser", "#fff", null, "hello", "", null, 0, null, new() { new("kekw", "assets/3x.webp") },
            ChatOriginEnum.Twtich, new() { AuthEnum.Undefined }, new() { SpecialMessgeEnum.Undefined }, EffectEnum.none, false, 0, DateTime.UtcNow);

        _twitchCallCache.AddMessage(message, CallCacheEnum.CachedMessageData);
    }

    [HttpGet("GetCacheData")]
    public async void GetChachedData([FromServices] ITwitchCallCache _twitchCallCache)
    {
        var t = _twitchCallCache.GetAllMessages(CallCacheEnum.CachedMessageData);

        List<MessageDto> messages = t.ConvertAll(s => (MessageDto)s);

        foreach (var message in messages)
        {
            Console.WriteLine(message.Date);
        }
    }

    [HttpDelete("DeleteMessage")]
    public async void DeleteMessage([FromServices] IHubContext<ChatHub> clientHub, string id)
    {
        BannedUserDto bannedUser = new(id, "userName", "message", "Reson", BannedTargetEnum.Message, DateTime.UtcNow);

        await clientHub.Clients.All.SendAsync("ReceiveBanned", bannedUser);
    }
}
