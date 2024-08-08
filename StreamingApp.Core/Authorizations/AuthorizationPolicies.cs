namespace CvTool.Api.Core.Authorizations;

public abstract record AuthorizationPolicies
{
    public const string Authenticated = "Authenticated";
    public const string Admin = "Admin";
    public const string Streamer = "Streamer";
    public const string TwitchMod = "TwitchMod";
    public const string YoutubeMod = "YoutubeMod";
}
