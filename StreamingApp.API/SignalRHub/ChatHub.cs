using Microsoft.AspNetCore.SignalR;

namespace StreamingApp.API.SignalRHub;

// Class for sending Chat messages to the Frontend with out a Frontend Request
public class ChatHub : Hub
{
    /// <summary>
    /// only used for testing an incomming message from the Frontend right now (Not working at the moment)
    /// Tecnicly not needed as the other API gateway can be used to (maybe later for direct messages for connected mods / users)
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessage(string message)
    {
        Console.WriteLine(message);
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "chat");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "chat");
        await base.OnDisconnectedAsync(exception);
    }
}
