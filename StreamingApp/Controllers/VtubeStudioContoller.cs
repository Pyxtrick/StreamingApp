using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StreamingApp.API.SignalRHub;
using StreamingApp.Core.VTubeStudio.Cache.Interface;
using StreamingApp.Domain.Entities.VtubeStudio;
using System.Text.Json;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VtubeStudioContoller : ControllerBase
{
    [HttpGet("GetModels")]
    public List<Model> GetModels([FromServices] IVtubeStudioCache _vtubeStudioCache)
    {
        return _vtubeStudioCache.GetModels();
    }

    [HttpPost("MoveOrChangeModel")]
    public async Task MoveOrChangeModel([FromServices] IHubContext<ClientHub> hubContext, MoveModelData moveModelData)
    {
        await hubContext.Clients.All.SendAsync("ReciveClientVtubeStudioModel", JsonSerializer.Serialize(moveModelData));
    }

    [HttpGet("GetToggles")]
    public List<Toggle> GetToggles([FromServices] IVtubeStudioCache _vtubeStudioCache)
    {
        return _vtubeStudioCache.GetToggles();
    }

    [HttpPost("TriggerToggle")]
    public async Task TriggerToggle([FromServices] IHubContext<ClientHub> hubContext, string toggleId)
    {
        await hubContext.Clients.All.SendAsync("ReciveClientVtubeStudioToggle", toggleId);
    }

    [HttpGet("GetItems")]
    public List<Item> GetItems([FromServices] IVtubeStudioCache _vtubeStudioCache)
    {
        return _vtubeStudioCache.GetItems();
    }

    [HttpPost("SetOrMoveOrDeletItem")]
    public async Task SetOrMoveOrDeletItem([FromServices] IHubContext<ClientHub> hubContext, Item item)
    {
        await hubContext.Clients.All.SendAsync("ReciveClientVtubeStudioItem", JsonSerializer.Serialize(item));
    }
}
