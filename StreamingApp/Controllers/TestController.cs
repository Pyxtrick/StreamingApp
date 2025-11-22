using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StreamingApp.API.SignalRHub;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Queries.Achievements;
using StreamingApp.Core.Queries.Alerts.Interfaces;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;
using StreamingApp.Domain.Responces;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpPost("sendChatDto")]
    public async void SendChatDto([FromServices] IHubContext<ChatHub> clientHub)
    {
        int t = new Random().Next(1, 50);

        Random random = new Random();

        Console.WriteLine($"messageId {t}");

        string mess = "hello test";
         const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        mess = new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());

        MessageDto chatMessage = new(false, "local", "userid",
            "Noodle_Snake_Bot_Test", "Noodle_Snake_Bot_Test", "#ff6b6b", new List<EmoteSet>(), new() { new("kekw", "https://static-cdn.jtvnw.net/badges/v1/5527c58c-fb7d-422d-b71b-f309dcb85cc1/3") }, OriginEnum.Twitch,
            new() { AuthEnum.Undefined }, new() { SpecialMessgeEnum.Undefined }, EffectEnum.none, false, 0, false, "Id", "replymessage", mess, "emoteReply", DateTime.Now);

        Console.WriteLine($"message {chatMessage.UserName}");

        await clientHub.Clients.All.SendAsync("ReceiveChatMessage", chatMessage);
        await clientHub.Clients.All.SendAsync("ReceiveOnScreenChatMessage", chatMessage);
    }

    [HttpPost("sendEmoteChatDto")]
    public async void SendEmoteChatDto([FromServices] IHubContext<ChatHub> clientHub)
    {
        int t = new Random().Next(1, 50);

        Random random = new Random();

        Console.WriteLine($"messageId {t}");

        MessageDto chatMessage = new(false, "local", "userid",
            "Noodle_Snake_Bot_Test", "Noodle_Snake_Bot_Test", "#ff6b6b", new List<EmoteSet>() { new EmoteSet() { Name = "tinyka2JamA", StaticURL = "", AnimatedURL = "https://static-cdn.jtvnw.net/emoticons/v2/emotesv2_b8d7d382937a49b2bbaad3bf6df4dabd/default/dark/4.0" } }, new() { new("kekw", "https://static-cdn.jtvnw.net/badges/v1/5527c58c-fb7d-422d-b71b-f309dcb85cc1/3") }, OriginEnum.Twitch,
            new() { AuthEnum.Undefined }, new() { SpecialMessgeEnum.Undefined }, EffectEnum.none, false, 0, false, "Id", "", "pyxtriRave pyxtriRave pyxtriRave pyxtriRave pyxtriRave pyxtriRave pyxtriRave pyxtriRave pyxtriRave pyxtriRave", "emoteReply", DateTime.Now);

        Console.WriteLine($"message {chatMessage.UserName}");

        await clientHub.Clients.All.SendAsync("ReceiveChatMessage", chatMessage);
        await clientHub.Clients.All.SendAsync("ReceiveOnScreenChatMessage", chatMessage);
    }

    [HttpPut("AddToChache")]
    public async void AddDataToCache([FromServices] ITwitchCallCache _twitchCallCache)
    {
        MessageDto message = new(false, "testuser", "TestUser", "1", "testuser", "#fff", null, new() { new("kekw", "assets/3x.webp") },
            OriginEnum.Twitch, new() { AuthEnum.Undefined }, new() { SpecialMessgeEnum.Undefined }, EffectEnum.none, false, 0, false, "1", null, "hello", "", DateTime.UtcNow);

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
        BannedUserDto bannedUser = new("userId", messageId, "userName", "message", "Reson", BannedTargetEnum.Message, false, OriginEnum.Twitch, DateTime.UtcNow);

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
        var t = await createFinalStreamAchievements.Execute();
        Console.WriteLine(t.Duration);
        await clientHub.Clients.All.SendAsync("ReceiveAlert", t);
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
            var alert = await subAlertLoong.Execute(k.Key, k.Value[0], k.Value[1], k.Value[2], true, false);

            html += $"<div>{alert.Html}</div>";
        }

        var finalAlert = new AlertDto() { Html = html, Duration = 15 };

        await clientHub.Clients.All.SendAsync("ReceiveAlert", finalAlert);
    }

    [HttpGet("StreamRaidAllert")]
    public async Task GetRaidAllert([FromServices] IRaidAlert raidAlert, IHubContext<ChatHub> clientHub, int count, string? image)
    {
        await clientHub.Clients.All.SendAsync("ReceiveAlert", await raidAlert.Execute(count, image));
    }

    [HttpGet("TextAlert")]
    public async Task TextAlert([FromServices] IMovingText movingText, int adLength, string text)
    {
        await movingText.ExecuteAlert(adLength, text);
    }
}
