using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StreamingApp.API.Twitch.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.PubSub;
using WebSocketSharp.Server;

namespace StreamingApp.API.Twitch;

public class TwitchInitialise : ITwitchInitialise
{
    private readonly ILogger<TwitchInitialise> _logger;

    private readonly ITwitchCache _twitchCache;
    private readonly ITwitchApiRequest _twichApiRequest;
    private readonly ITwitchPubSubApiRequest _twitchPubSubApiRequest;
    private readonly IConfiguration _configuration;

    // Authentication
    private HttpServer WebServer;
    private readonly List<string> Scopes = new List<string> {
        "user:bot", "user:write:chat", "user:edit", "user:read:chat", "user:read:email", "user:read:subscriptions", 
        "moderator:manage:shoutouts", "moderator:manage:announcements", "moderator:read:followers", "moderator:read:chat_messages",
        "chat:read", "chat:edit", 
        "whispers:read", "whispers:edit",
        "bits:read",
        "channel:bot", "channel:moderate", "channel:read:subscriptions", "channel:read:goals", "channel:read:polls", "channel:read:hype_train", "channel:read:predictions", "channel:read:redemptions", "channel:read:ads", "channel:manage:ads" };

    // TwichLib
    private TwitchClient OwnerOfChannelConnection;
    private TwitchPubSub Pubsub;
    private TwitchAPI TheTwitchAPI;

    // Cached Variables
    private string CachedOwnerOfChanelAccessToken = "needsaccesstoken"; // Change to AppAccess Tocken
    private string TwitchChannelName;

    public TwitchInitialise(ILogger<TwitchInitialise> logger, ITwitchApiRequest twichApiRequest, ITwitchCache twitchCache, IConfiguration configuration, ITwitchPubSubApiRequest twitchPubSubApiRequest)
    {
        _logger = logger;
        _twichApiRequest = twichApiRequest;
        _twitchCache = twitchCache;
        _configuration = configuration;
        _twitchPubSubApiRequest = twitchPubSubApiRequest;
    }

    public void StartTwitchBot()
    {
        InitializeWebServer();

        // open browser to Authenticate Bot
        // TODO: update ClientId and ClientSecret in UserSecrets to use the PyxtrickBot Account and not the Pyxtrick Account
        var authUrl = $"https://id.twitch.tv/oauth2/authorize?response_type=code&client_id={_configuration["Twitch:ClientId"]}&redirect_uri={_configuration["Twitch:RedirectUrl"]}&scope={String.Join("+", Scopes)}";
        var authUrl2 = $"https://id.twitch.tv/oauth2/token?id={_configuration["Twitch:ClientId"]}";
        //For Default Browser:
        //Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true });
        Process.Start(new ProcessStartInfo() { UseShellExecute = true, FileName = "chrome.exe", Arguments = authUrl });
        //Process.Start(new ProcessStartInfo() { UseShellExecute = true, FileName = "chrome.exe", Arguments = authUrl2 });
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
                InitalizePubSub(CachedOwnerOfChanelAccessToken);
                InitializeTwitchAPI(CachedOwnerOfChanelAccessToken);
            }
        };

        WebServer.Start();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Web server started on: {WebServer}");
        Console.ResetColor();
    }

    public void CloseConntection(object sender, EventArgs e)
    {
        if(Pubsub != null)
        {
            Pubsub.Disconnect();
        }

        if (OwnerOfChannelConnection != null)
        {
            OwnerOfChannelConnection.Disconnect();
        }

        if (WebServer != null)
        {
            WebServer.Stop();
        }
    }

    private async Task<Tuple<string, string, string>> GetAccessAndRefreshToken(string code)
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

        /** app access token*/
        // https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#client-credentials-grant-flow -> client_credentials
        HttpClient appClient = new HttpClient();
        var appValues = new Dictionary<string, string>
        {
            { "client_id", _configuration["Twitch:ClientId"] },
            { "client_secret", _configuration["Twitch:ClientSecret"] },
            { "grant_type", "client_credentials" },
        };

        var appResponse = await appClient.PostAsync("https://id.twitch.tv/oauth2/token", new FormUrlEncodedContent(appValues));
        var appJson = JsonObject.Parse(await appResponse.Content.ReadAsStringAsync());
        
        return new Tuple<string, string, string>(json["access_token"].ToString(), json["refresh_token"].ToString(), appJson["access_token"].ToString());
    }

    private async Task SetNameAndOuthedUser(string accessToken)
    {
        var api = new TwitchAPI();
        api.Settings.ClientId = _configuration["Twitch:ClientId"];
        api.Settings.AccessToken = accessToken;

        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await client.GetAsync("https://id.twitch.tv/oauth2/validate");

        var responseString = await response.Content.ReadAsStringAsync();
        var json = JsonObject.Parse(responseString);

        //TwitchChannelName = "noodlesnekbot";

        var outhedUser = await api.Helix.Users.GetUsersAsync();
        TwitchChannelName = outhedUser.Users[0].Login;
    }

    private void InitalizePubSub(string accessToken)
    {
        Pubsub = new TwitchPubSub();

        Pubsub.OnPubSubServiceConnected += Bot_OnPubSubServiceConnected;
        Pubsub.OnLog += _twitchPubSubApiRequest.Bot_OnLog;
        Pubsub.OnChannelPointsRewardRedeemed += _twitchPubSubApiRequest.Bot_OnRewardRedeemed;
        //Pubsub.OnFollow += _twitchPubSubApiRequest.Bot_OnFollow;

        Pubsub.Connect();
    }

    private void Bot_OnPubSubServiceConnected(object sender, EventArgs e)
    {
        Pubsub.SendTopics(CachedOwnerOfChanelAccessToken);
        Pubsub.ListenToChannelPoints(_configuration["Twitch:ChannelId"]);
        //Pubsub.ListenToFollows(_configuration["Twitch:ChannelId"]);
        //Pubsub.ListenToChatModeratorActions(_configuration["Twitch:ChannelBotId"], _configuration["Twitch:ChannelId"]);

        Console.WriteLine("PubSub Connected");
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
        OwnerOfChannelConnection.OnChannelStateChanged += _twichApiRequest.Bot_OnChannelStateChanged; // TODO: Check on what it is doing 

        OwnerOfChannelConnection.OnUserBanned += _twichApiRequest.Bot_OnUserBanned;
        OwnerOfChannelConnection.OnUserTimedout += _twichApiRequest.Bot_OnUserTimedout;
        OwnerOfChannelConnection.OnMessageCleared += _twichApiRequest.Bot_OnMessageCleared;
        //OwnerOfChannelConnection.OnDuplicate
        // TODO: Not there jet OwnerOfChannelConnection.OnNewFollower

        // check for add's / advertisements

        #region toTest
        OwnerOfChannelConnection.OnUserJoined += _twichApiRequest.Bot_OnUserJoined; // TODO: Check on what it is doing if follows also count and other then chatting users
        OwnerOfChannelConnection.OnUnaccountedFor += _twichApiRequest.Bot_OnUnaccountedFor;
        OwnerOfChannelConnection.OnSendReceiveData += _twichApiRequest.Bot_OnSendReceiveData;
        #endregion

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
