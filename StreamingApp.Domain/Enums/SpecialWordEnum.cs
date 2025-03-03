namespace StreamingApp.Domain.Enums;

public enum SpecialWordEnum
{
    None = 0,
    Delete,
    Timeout,
    Replace,
    Banned, // For banned words
    AllowedUrl, // Allowing url's to be used and ignore Delete / timeout
    Count,
    Special,
    Keyword,
    Spam,
}
