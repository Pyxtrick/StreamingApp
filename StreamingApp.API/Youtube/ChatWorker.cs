using Microsoft.Extensions.Hosting;
using YTLiveChat.Contracts.Models;
using YTLiveChat.Contracts.Services;

namespace StreamingApp.API.Youtube;

public sealed class ChatWorker(IYTLiveChat chat) : IHostedService
{
    public Task StartAsync(CancellationToken ct)
    {
        chat.InitialPageLoaded += (_, e) => Console.WriteLine($"Loaded: {e.LiveId}");
        chat.ChatReceived += (_, e) => HandleChat(e.ChatItem);
        chat.GiftReceived += (_, e) => HandleGifted(e.Gift);
        //chat.LivestreamStarted += (_, e) => HandleStreamStart(e.LiveId);
        //chat.LivestreamEnded += (_, e) => HandleStreamEnd(e.LiveId);
        chat.ChatItemDeleted += (_, e) => HandleMessageDeleted(e);
        chat.RawActionReceived += (_, e) =>
        {
            if (e.ParsedChatItem is null)
            {
                // Unsupported action still available here
                Console.WriteLine("RAW action received.");
            }
        };
        //chat.ChatStopped += (_, e) => Console.WriteLine($"Stopped: {e.Reason}");
        chat.ErrorOccurred += (_, e) => Console.WriteLine($"Error: {e.GetException().Message}");

        chat.Start(handle: "@pyxtrick");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken ct)
    {
        chat.Stop();
        return Task.CompletedTask;
    }

    private static void HandleChat(ChatItem item)
    {
        Console.WriteLine("YT Message", item.Message);
        // inspect item.Superchat, item.MembershipDetails, item.ViewerLeaderboardRank, item.IsTicker
        // for ticker events: item.Author.ChannelId, item.Author.Thumbnail, item.Author.ChannelHandle
    }

    private static void HandleMessageDeleted(ChatItemDeletedEventArgs e)
    {
        Console.WriteLine("YT Message Deleteted", e.TargetId);
    }

    private static void HandleGifted(GiftItem item)
    {
        Console.WriteLine("YT Gifted", $"{item.AuthorHandle} Gifted with Text {item.Text}");
    }

    private static void HandleStreamStart(string item)
    {
        Console.WriteLine($"Stream Started with ID {item}");
    }

    private static void HandleStreamEnd(string item)
    {
        Console.WriteLine($"Stream Ended with ID {item}");
    }
}