﻿using Microsoft.AspNetCore.Mvc;
using StreamingApp.API.StreamerBot;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.Core.Commands.Twitch.Interfaces;
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

    [HttpGet("StartStream")]
    public async Task StartStream([FromServices] IManageStream updateStream, string youtubeId)
    {
        await updateStream.StartStream(youtubeId);
        //TODO: send to YouTube as well
    }

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
        MessageDto messsage = new(null, true, broadcastUserName, userId, userName, userName, "", null, message, null, null, null, OriginEnum.Youtube, null, null, EffectEnum.none, false, 0, false, DateTime.Now);

        await manageMessages.ExecuteOne(messsage);
    }

    [HttpGet("Ads")]
    public async Task Ads(int adLength)
    {
        Console.WriteLine(adLength.ToString());
    }

    [HttpGet("HypeTrain")]
    public async Task HypeTrain(int level, float persentage, string hypeTrainStage, int bitsCount, int subsCount)
    {
        switch (hypeTrainStage)
        {
            case "HypeTrain Start": // Start
                break;
            case "HypeTrain": // Update
                break;
            case "HypeTrain Level": // Level Up
                break;
            case "HypeTrain End": // End
                break;
        }
    }
}
