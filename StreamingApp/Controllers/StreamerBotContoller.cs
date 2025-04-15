using Microsoft.AspNetCore.Mvc;

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
}
