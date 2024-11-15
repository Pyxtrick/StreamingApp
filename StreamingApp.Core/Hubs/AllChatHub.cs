﻿using Microsoft.AspNetCore.SignalR;
using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Hubs;

// Class for sending Chat messages
public class AllChatHub : Hub
{
    public async Task SendMessage(ChatDto message)
    {
        await Clients.All.SendAsync("ReceiveAllChat", message);
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "chat");
        await Clients.Caller.SendAsync("UserConnectedAllChat");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "chat");
        await base.OnDisconnectedAsync(exception);
    }
}
