namespace StreamingApp.Domain.Entities.Internal.Trigger;

public class TargetData
{
    public int Id { get; set; }

    public int TargetId { get; set; }
    public Target Target { get; set; }

    public string Text { get; set; }

    public int Duration { get; set; }

    public int EmoteId { get; set; }
    public Emote Emote { get; set; }

    public int Size { get; set; }
}
