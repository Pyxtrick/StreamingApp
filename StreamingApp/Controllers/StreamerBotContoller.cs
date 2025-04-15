using Microsoft.AspNetCore.Mvc;
using StreamingApp.API.StreamerBot;

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
}
