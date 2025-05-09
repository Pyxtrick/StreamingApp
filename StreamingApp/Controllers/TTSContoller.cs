using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.Utility.TextToSpeach;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TTSContoller : ControllerBase
{
    [HttpPost("PlaySpecificTTSMessage")]
    public async Task PlaySpecificTTSMessage([FromServices] IManageTextToSpeach _manageTextToSpeach, int id)
    {
        await _manageTextToSpeach.PlaySpecificTTSMessage(id);
    }

    [HttpPost("PlayLatestTTSMessage")]
    public async Task PlayLatestTTSMessage([FromServices] IManageTextToSpeach _manageTextToSpeach)
    {
        await _manageTextToSpeach.PlayLatestTTSMessage();
    }
}
