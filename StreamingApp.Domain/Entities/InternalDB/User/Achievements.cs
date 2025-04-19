namespace StreamingApp.Domain.Entities.InternalDB.User;

public class Achievements // TODO: Change all DB to AchievementsEntity
{
    public int Id { get; set; }

    public User User { get; set; }

    // Giffted Subs Amount
    public int GiftedSubsCount { get; set; }

    // Gifted Bits Amount
    public int GiftedBitsCount { get; set; }

    // Gifted Donation Amount
    public int GiftedDonationCount { get; set; }

    // Has watched how many streams (using the message during a stream)
    public int WachedStreams { get; set; }

    // Date of the Last Stream Seen / chatted
    public DateTime LastStreamSeen { get; set; }

    public DateTime FirstStreamSeen { get; set; }
}
