using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.Commands.DB.Interfaces;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StreamController : ControllerBase
{
    [HttpPost("StartOrEndStream")]
    public void StartOrEndStream([FromServices] IUpdateStream updateStream, string streamTitle, string categoryName)
    {
        updateStream.StartOrEndStream(streamTitle, categoryName);
    }

    [HttpPost("ChangeCategory")]
    public void ChangeCategory([FromServices] IUpdateStream updateStream, string categoryName)
    {
        updateStream.ChangeCategory(categoryName);
    }
}
