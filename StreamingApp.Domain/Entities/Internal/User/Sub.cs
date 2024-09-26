﻿using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Internal.User;

public class Sub
{
    public int Id { get; set; }

    public Status Status { get; set; }

    // Sub has been gifted to person
    public bool SubGiffted { get; set; }

    // Sub by the Person
    public bool CurrentySubscribed { get; set; }

    // Months Subs
    public int SubscribedTime { get; set; }

    // Tier of current sub
    public TierEnum CurrentTier { get; set; }
}