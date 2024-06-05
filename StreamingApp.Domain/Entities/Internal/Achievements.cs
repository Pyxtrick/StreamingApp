namespace StreamingApp.Domain.Entities.Internal;

public class Achievements
{
    public int Id { get; set; }

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
}
