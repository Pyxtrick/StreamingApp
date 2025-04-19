using Microsoft.AspNetCore.Mvc;
using StreamingApp.API.StreamerBot;
using StreamingApp.Core.Commands.Twitch.Interfaces;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StreamerBotContoller : ControllerBase
{
    [HttpGet("StreamerBot")]
    public string Testing(string user, string text)
    {
        Console.WriteLine($"{user}, {text}");

        return "true";
    }

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
}
