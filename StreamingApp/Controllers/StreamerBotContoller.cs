using Microsoft.AspNetCore.Mvc;
using StreamingApp.API.StreamerBot;
using StreamingApp.Core.Commands.Bluesky;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Core.Queries.Alerts.Interfaces;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StreamerBotContoller : ControllerBase
{
    [HttpGet("GetActions")]
    public async Task<List<Actions>> GetActions([FromServices] IStreamerBotRequest streamerBotRequest)
    {
        return await streamerBotRequest.GetActions();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateStream"></param>
    /// <param name="manageTweets"></param>
    /// <param name="youtubeId"></param>
    /// <returns></returns>
    [HttpGet("StartStream")]
    public async Task StartStream([FromServices] IManageStream updateStream, [FromServices] IManageTweets manageTweets, string youtubeId)
    {
        await updateStream.StartStream(youtubeId);


        List<KeyValuePair<OriginEnum, string>> test = new List<KeyValuePair<OriginEnum, string>>() { new(OriginEnum.Twitch, ""), new(OriginEnum.Youtube, youtubeId) };

        await manageTweets.SendStreamStartTweet(test);
    }

    //http://localhost:7033/api/StreamerBotContoller/UpdateCategory?category=%gameName%
    [HttpGet("UpdateCategory")]
    public async Task StreamCategory([FromServices] IManageStream updateStream, string category)
    {
        await updateStream.ChangeCategory();
    }

    [HttpGet("FirstMessage")]
    public async Task FirstMessage([FromServices] ICRUDUsers crudUsers, string userId)
    {
        await crudUsers.UpdateAchievements(userId, OriginEnum.Youtube);
    }

    [HttpGet("Command")]
    public async Task Command([FromServices] IManageMessages manageMessages, string userId, string userName, string message, string broadcastUserName, bool isModerator)
    {
        MessageDto messsage = new(true, broadcastUserName, userId, userName, userName, null, null, null, OriginEnum.Youtube, null, null, EffectEnum.none, false, 0, false, null, "", null, message, DateTime.Now);

        await manageMessages.ExecuteOne(messsage);
    }

    //http://localhost:7033/api/StreamerBotContoller/Ads?adLength=%adLength%
    [HttpGet("Ads")]
    public async Task Ads([FromServices] IMovingText movingText, int adLength)
    {
        await movingText.ExecuteAlert(adLength, "Stream is Paused for Ads");
    }

    //http://localhost:7033/api/StreamerBotContoller/PointRedeam?userName=%userName%&userId=%userId%&rewardid=%rewardId%&rewardName=%rewardName%&rewardPrompt=%rewardPrompt%
    [HttpGet("PointRedeam")]
    public async Task PointRedeam([FromServices] IPointRedeam pointRedeam, string userName, string userId, string rewardid, string rewardName, string? rewardPrompt)
    {
        await pointRedeam.Execute(userName, userId, rewardid, rewardName, rewardPrompt);
    }

    //http://localhost:7033/api/StreamerBotContoller/Raid?userName=%userName%&raidSize=%viewers%
    [HttpGet("Raid")]
    public async Task RaidAllert([FromServices] IManageAlert manageAlert, string userName, int raidSize)
    {
        RaidDto raid = new(userName, raidSize, "", false, DateTime.UtcNow);

        await manageAlert.ExecuteRaid(raid);
    }

    //http://localhost:7033/api/StreamerBotContoller/HypeTrain?level=%level%&persentage=%percentDecimal%&hypeTrainStage=%triggerName%&isGoldenKappaTrain=%isGoldenKappaTrain%
    [HttpGet("HypeTrain")]
    public async Task HypeTrain(int level, float persentage, string hypeTrainStage, bool isGoldenKappaTrain)
    {
        switch (hypeTrainStage)
        {
            case "Hype Train Start":
                break;
            case "Hype Train Update":
                break;
            case "Hype Train Level Up":
                break;
            case "Hype Train End":
                break;
        }
    }
}
