using Microsoft.AspNetCore.SignalR;
using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Hubs;

public class AllChatHub : Hub
{
    public async Task SendMessage(ChatDto message)
    {
        await Clients.All.SendAsync("ReceiveAllChat", message);
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("UserConnectedAllChat");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
