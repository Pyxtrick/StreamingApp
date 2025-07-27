using Microsoft.AspNetCore.SignalR;
using StreamingApp.API.SignalRHub;
using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Queries.Alerts;

public class MovingText : IMovingText
{
    private readonly IHubContext<ChatHub> clientHub;

    public MovingText(IHubContext<ChatHub> clientHub)
    {
        this.clientHub = clientHub;
    }

    public async Task ExecuteAlert(int adLength, string text)
    {
        string p = "";
        foreach (var word in text.Split(" "))
        {
            p += $"<span>{word}</span>";
        }

        var ads = "<body>" +
            $"<p class=\"marquee\">{p}</p>" +
            "<style>" +
                ".marquee { width: 100%; margin: 0 auto; overflow: hidden; white-space: nowrap; color: white; height: 100px; } .marquee span { display: inline-block; font-size: 20px; position: relative; left: 100%; animation: marquee 15s linear infinite; }  .marquee span:nth-child(1) { animation-delay: 0s; } .marquee span:nth-child(2) { animation-delay: 0.5s; } .marquee span:nth-child(3) { animation-delay: 1s; } .marquee span:nth-child(4) { animation-delay: 1.5s; } .marquee span:nth-child(5) { animation-delay: 2s; }  .marquee span { top: 40px; font-size: 100px; }  @keyframes marquee { 0% {   left: 100%; } 100% {   left: -55%; } }" +
            "</style>" +
        "</body>";

        await clientHub.Clients.All.SendAsync("receiveOnscreenMessage", new AlertDto() { Html = ads, Duration = adLength });
    }
}
