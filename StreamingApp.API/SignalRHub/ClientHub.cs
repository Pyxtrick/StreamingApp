using Microsoft.AspNetCore.SignalR;

namespace StreamingApp.API.SignalRHub;

// Class for sending Chat messages to the Client App
public class ClientHub : Hub
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessage(string message)
    {
        Console.WriteLine(message);
        await Clients.All.SendAsync("ReceiveClientMessage", message);
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "client");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "client");
        await base.OnDisconnectedAsync(exception);
    }
}
