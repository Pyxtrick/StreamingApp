using Microsoft.AspNetCore.SignalR;
using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Hubs;

// Class for sending Events
public class ClientHub : Hub
{
    public async Task SendMessage(ChatDto message)
    {
        await Clients.All.SendAsync("ReceiveClient", message);
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "client");
        await Clients.Caller.SendAsync("UserConnectedClient");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "client");
        await base.OnDisconnectedAsync(exception);
    }
}
