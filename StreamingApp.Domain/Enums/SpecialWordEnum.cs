﻿namespace StreamingApp.Domain.Enums;

public enum SpecialWordEnum
{
    None = 0,
    Delete,
    Timeout,
    Banned, // For banned words
    Allowed, // Allowing url's to be used 
    Count,
    Special,
    Keyword,
    Spam,
}
