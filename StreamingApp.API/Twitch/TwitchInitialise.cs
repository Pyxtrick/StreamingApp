using Microsoft.Extensions.Configuration;
using StreamingApp.API.Twitch.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;
using System.Diagnostics;
using System.Net;
using System.Text.Json.Nodes;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using WebSocketSharp.Server;

namespace StreamingApp.API.Twitch;

public class TwitchInitialise : ITwitchInitialise
{
    private readonly ITwitchCache _twitchCache;
    private readonly ITwitchApiRequest _twichApiRequest;
    private readonly IConfiguration _configuration;

    // Authentication
    private HttpServer WebServer;
    private readonly List<string> Scopes = new List<string> { "user:edit", "user:read:chat", "user:read:email", "user:read:subscriptions", "moderator:manage:shoutouts", "chat:read", "chat:edit", "whispers:read", "whispers:edit", "channel:moderate", "channel:read:subscriptions", "channel:read:goals", "channel:read:polls", "channel:read:hype_train", "channel:read:predictions", "channel:read:redemptions" };

    // TwichLib
    private TwitchClient OwnerOfChannelConnection;
    private TwitchAPI TheTwitchAPI;

    // Cached Variables
    private string CachedOwnerOfChanelAccessToken = "needsaccesstoken";
    private string TwitchChannelName;
    private string TwitchChannelId;

    public TwitchInitialise(ITwitchApiRequest twichApiRequest, ITwitchCache twitchCache, IConfiguration configuration)
    {
        _twichApiRequest = twichApiRequest;
        _twitchCache = twitchCache;
        _configuration = configuration;
    }

    public void StartTwitchBot()
    {
        InitializeWebServer();

        // open browser to Authenticate Bot
        // TODO: update ClientId and ClientSecret in UserSecrets to use the PyxtrickBot Account and not the Pyxtrick Account
        var authUrl = $"https://id.twitch.tv/oauth2/authorize?response_type=code&client_id={_configuration["Twitch:ClientId"]}&redirect_uri={_configuration["Twitch:RedirectUrl"]}&scope={String.Join("+", Scopes)}";
        //For Default Browser:
        //Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true });
        Process.Start(new ProcessStartInfo() { UseShellExecute = true, FileName = "chrome.exe", Arguments = authUrl });
    }

    public void InitializeWebServer()
    {
        WebServer = new HttpServer(IPAddress.Loopback, 80);
        var t = IPAddress.Loopback;

        WebServer.OnGet += async (s, e) =>
        {
            using StreamWriter writer = new StreamWriter(e.Response.OutputStream);
            if (e.Request.QueryString.AllKeys.Any("code".Contains))
            {
                var code = e.Request.QueryString["code"];
                var ownerOfChannelAccessAndRefresh = await GetAccessAndRefreshToken(code!);
                CachedOwnerOfChanelAccessToken = ownerOfChannelAccessAndRefresh.Item1;
                SetNameAndOuthedUser(CachedOwnerOfChanelAccessToken).Wait();
                InitializeOwnerOfChannelConnection(TwitchChannelName, CachedOwnerOfChanelAccessToken);
                InitializeTwitchAPI(CachedOwnerOfChanelAccessToken);
            }
        };

        WebServer.Start();
        Console.WriteLine($"Web server started on: {WebServer}");
    }

    public void CloseConntection(object sender, EventArgs e)
    {
        if (OwnerOfChannelConnection != null)
        {
            OwnerOfChannelConnection.Disconnect();
        }

        if (WebServer != null)
        {
            WebServer.Stop();
        }
    }

    private async Task<Tuple<string, string>> GetAccessAndRefreshToken(string code)
    {
        HttpClient client = new HttpClient();
        var values = new Dictionary<string, string>
        {
            { "client_id", _configuration["Twitch:ClientId"] },
            { "client_secret", _configuration["Twitch:ClientSecret"] },
            { "code", code },
            { "grant_type", "authorization_code" },
            { "redirect_uri", _configuration["Twitch:RedirectUrl"] },
        };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);

        var responseString = await response.Content.ReadAsStringAsync();
        var json = JsonObject.Parse(responseString);
        return new Tuple<string, string>(json["access_token"].ToString(), json["refresh_token"].ToString());
    }

    private async Task SetNameAndOuthedUser(string accessToken)
    {
        var api = new TwitchAPI();
        api.Settings.ClientId = _configuration["Twitch:ClientId"];
        api.Settings.AccessToken = accessToken;

        var outhedUser = await api.Helix.Users.GetUsersAsync();
        TwitchChannelId = outhedUser.Users[0].Id;
        TwitchChannelName = outhedUser.Users[0].Login;

        _twitchCache.AddTwitchChannelName(_configuration["Twitch:Channel"], _configuration["Twitch:ChannelId"]);
    }

    private void InitializeOwnerOfChannelConnection(string username, string accessToken)
    {
        OwnerOfChannelConnection = new TwitchClient();
        OwnerOfChannelConnection.Initialize(new ConnectionCredentials(username, accessToken), TwitchChannelName);

        // Events you want to subscribe to
        OwnerOfChannelConnection.OnConnected += _twichApiRequest.Client_OnConnected;
        OwnerOfChannelConnection.OnDisconnected += _twichApiRequest.OwnerOfChannel_OnDisconnected;

        // All messages can be usefull during debug
        //OwnerOfChannelConnection.OnLog = _twichApiRequest.OwnerOfChannelConnection_OnLog;
        OwnerOfChannelConnection.OnMessageReceived += _twichApiRequest.Bot_OnMessageReceived;
        OwnerOfChannelConnection.OnChatCommandReceived += _twichApiRequest.Bot_OnChatCommandRecived;

        OwnerOfChannelConnection.OnGiftedSubscription += _twichApiRequest.Bot_OnGiftedSubscription;
        //OwnerOfChannelConnection.OnContinuedGiftedSubscription += _twichApiRequest.Bot_OnContinuedGiftedSubscription;
        OwnerOfChannelConnection.OnNewSubscriber += _twichApiRequest.Bot_OnNewSubscriber;
        OwnerOfChannelConnection.OnPrimePaidSubscriber += _twichApiRequest.Bot_OnPrimePaidSubscriber;
        OwnerOfChannelConnection.OnReSubscriber += _twichApiRequest.Bot_OnReSubscriber;

        OwnerOfChannelConnection.OnRaidNotification += _twichApiRequest.Bot_OnRaidNotification;
        OwnerOfChannelConnection.OnUserJoined += _twichApiRequest.Bot_OnUserJoined; // TODO: Check on what it is doing if follows also count and other then chatting users
        OwnerOfChannelConnection.OnChannelStateChanged += _twichApiRequest.Bot_OnChannelStateChanged; // TODO: Check on what it is doing 

        OwnerOfChannelConnection.OnUserBanned += _twichApiRequest.Bot_OnUserBanned;
        OwnerOfChannelConnection.OnUserTimedout += _twichApiRequest.Bot_OnUserTimedout;
        OwnerOfChannelConnection.OnMessageCleared += _twichApiRequest.Bot_OnMessageCleared;
        //OwnerOfChannelConnection.OnDuplicate
        // TODO: Not there jet OwnerOfChannelConnection.OnNewFollower

        // check for add's / advertisements

        OwnerOfChannelConnection.Connect();
        OwnerOfChannelConnection.JoinChannel(_configuration["Twitch:Channel"]);

        _twitchCache.AddData(OwnerOfChannelConnection, TheTwitchAPI);
    }

    private void InitializeTwitchAPI(string accessToken)
    {
        TheTwitchAPI = new TwitchAPI();
        TheTwitchAPI.Settings.ClientId = _configuration["Twitch:ClientId"];
        TheTwitchAPI.Settings.AccessToken = accessToken;
        _twitchCache.AddData(null, TheTwitchAPI);
    }
}
