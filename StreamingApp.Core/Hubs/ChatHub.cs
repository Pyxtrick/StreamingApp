using Microsoft.AspNetCore.SignalR;

namespace StreamingApp.Core.Hubs;

// Class for sending Chat messages
public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "chat");
        await Clients.Caller.SendAsync("ReceiveChatMessage");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "chat");
        await base.OnDisconnectedAsync(exception);
    }
}
