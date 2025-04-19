namespace StreamingApp.Domain.Entities.InternalDB.Stream;

public class Choice
{
    public int Id { get; set; }

    public int PoleId { get; set; }
    public Pole Poles { get; set; }

    public string Title { get; set; }

    public int Votes { get; set; }

    public int VotesPoints { get; set; }

    public int ChannelPointsVotes { get; set; }

    public int BitsVotes { get; set; }
}
