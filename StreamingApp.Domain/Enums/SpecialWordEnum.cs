namespace StreamingApp.Domain.Enums;

public enum SpecialWordEnum
{
    None = 0,
    Banned, // For banned words
    Timeout,
    Delete,
    Replace,
    AllowedUrl, // Allowing url's to be used and ignore Delete / timeout
    Spam,
    Count,
    Special,
    Keyword,
}
