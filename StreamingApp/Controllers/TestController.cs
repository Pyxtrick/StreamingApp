using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StreamingApp.API.SignalRHub;
using StreamingApp.Domain.Enums;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Queries.Achievements;
using StreamingApp.Domain.Responces;
using StreamingApp.Core.Queries.Alerts;
using StreamingApp.Domain.Entities.InternalDB.User;

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

        MessageDto chatMessage = new("Id", false, "local", "userid", 
            "testuser", "TestUser", "#fff", "replymessage", mess, "emoteReply", new List<EmoteSet>(), new() { new("kekw", "assets/3x.webp") }, ChatOriginEnum.Twitch,
            new() { AuthEnum.Undefined }, new() { SpecialMessgeEnum.Undefined }, EffectEnum.none, false, 0, false, DateTime.Now);

        Console.WriteLine($"message {chatMessage.UserName}");

        await clientHub.Clients.All.SendAsync("ReceiveChatMessage", chatMessage);
        await clientHub.Clients.All.SendAsync("ReceiveOnScreenChatMessage", chatMessage);
    }

    [HttpPut("AddToChache")]
    public async void AddDataToCache([FromServices] ITwitchCallCache _twitchCallCache)
    {
        MessageDto message = new("1", false, "testuser", "TestUser", "1", "testuser", "#fff", null, "hello", "", null, new() { new("kekw", "assets/3x.webp") },
            ChatOriginEnum.Twitch, new() { AuthEnum.Undefined }, new() { SpecialMessgeEnum.Undefined }, EffectEnum.none, false, 0, false, DateTime.UtcNow);

        _twitchCallCache.AddMessage(message, CallCacheEnum.CachedMessageData);
    }

    [HttpGet("GetCacheData")]
    public async Task<CacheResponse> GetChachedData([FromServices] ITwitchCallCache _twitchCallCache, IEmotesCache emotesCache)
    {
        CacheResponse cacheResponse = new()
        {
            messages = _twitchCallCache.GetAllMessages(CallCacheEnum.CachedMessageData, false).ConvertAll(s => (MessageDto)s),
            subs = _twitchCallCache.GetAllMessages(CallCacheEnum.CachedSubData, false).ConvertAll(s => (SubDto)s),
            alerts = _twitchCallCache.GetAllMessages(CallCacheEnum.CachedAlertData, false).ConvertAll(s => (AlertDto)s),
            raids = _twitchCallCache.GetAllMessages(CallCacheEnum.CachedRaidData, false).ConvertAll(s => (RaidDto)s),
            emotes = emotesCache.GetEmotes(null),
        };

        return cacheResponse;
    }

    [HttpDelete("DeleteMessage")]
    public async void DeleteMessage([FromServices] IHubContext<ChatHub> clientHub, string messageId)
    {
        BannedUserDto bannedUser = new("userId", messageId, "userName", "message", "Reson", BannedTargetEnum.Message, false, ChatOriginEnum.Twitch, DateTime.UtcNow);

        await clientHub.Clients.All.SendAsync("ReceiveBanned", bannedUser);
    }

    [HttpPost("SendToClient")]
    public async void SendToClient([FromServices] IHubContext<ClientHub> hubContext)
    {
        await hubContext.Clients.All.SendAsync("ReciveClientTTSMessage", "[mmaa<200,22>mmaa<1000,22>uw<400,20>uw<200,22>uw<200,23>uw<600,22]~");
    }

    [HttpGet("StreamAchievements")]
    public async Task GetStreamAchievements([FromServices] ICreateFinalStreamAchievements createFinalStreamAchievements, IHubContext<ChatHub> clientHub)
    {
        AlertDto alert = new AlertDto() { Html = await createFinalStreamAchievements.Execute() };

        await clientHub.Clients.All.SendAsync("ReceiveAlert", alert);
    }

    [HttpGet("StreamAllert")]
    public async Task GetStreamAllert([FromServices] ISubAlertLoong subAlertLoong, IHubContext<ChatHub> clientHub)
    {
        List<KeyValuePair<string, List<int>>> data = new()
        {
            new("Pyxtrick", new(){ new Random().Next(1, 150), new Random().Next(1, 360), new Random().Next(1, 1000) }),
            new("tiny_karo", new(){ new Random().Next(1, 150), new Random().Next(1, 360), new Random().Next(1, 1000) }),
            new("yamakasi", new(){ new Random().Next(1, 150), new Random().Next(1, 360), new Random().Next(1, 1000) }),
            new("PyxtrickBot", new(){ new Random().Next(1, 150), new Random().Next(1, 360), new Random().Next(1, 1000) }),
            new("servy_bot", new(){ new Random().Next(1, 150), new Random().Next(1, 360), new Random().Next(1, 1000) }),
        };

        string html = "";

        foreach (var k in data)
        {
            var alert = await subAlertLoong.Execute(k.Key, k.Value[0], k.Value[1], k.Value[2], true);

            html += $"<div>{alert.Html}</div>";
        }

        var finalAlert = new AlertDto() { Html = html, Duration = 15 };

        await clientHub.Clients.All.SendAsync("ReceiveAlert", finalAlert);
    }
}
