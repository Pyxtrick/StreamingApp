using Microsoft.AspNetCore.SignalR;
using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Hubs;

// Class for sending Events like Sub / Bit / Resub / Fallow / other Events
public class EventHub : Hub
{
    public async Task SendMessage(ChatDto message)
    {
        await Clients.All.SendAsync("ReceiveEvent", message);
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "event");
        await Clients.Caller.SendAsync("UserConnectedEvent");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "event");
        await base.OnDisconnectedAsync(exception);
    }
}
