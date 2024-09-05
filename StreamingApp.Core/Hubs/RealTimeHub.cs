using Microsoft.AspNetCore.SignalR;

namespace StreamingApp.Core.Hubs;

public class RealTimeHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "chat");
        await Clients.Caller.SendAsync("UserConnected1");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "chat");
        await base.OnDisconnectedAsync(exception);
    }
}
