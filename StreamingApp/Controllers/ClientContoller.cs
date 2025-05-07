using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.VTubeStudio.Cache.Interface;
using StreamingApp.Domain.Entities.VtubeStudio;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientContoller : ControllerBase
{
    [HttpPost("VtubeStudioData")]
    public async Task UpdateVtubeStudioData([FromServices] IVtubeStudioCache _vtubeStudioCache, VtubeStudioData vtubeStudioData)
    {
        _vtubeStudioCache.AddAllData(vtubeStudioData);
    }

    [HttpGet("VtubeStudioData")]
    public async Task<VtubeStudioData> GetVtubeStudioData([FromServices] IVtubeStudioCache _vtubeStudioCache)
    {
        return _vtubeStudioCache.AllData();
    }
}
