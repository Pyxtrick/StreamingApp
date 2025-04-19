namespace StreamingApp.Domain.Entities.InternalDB.Trigger;

public class TargetData
{
    public int Id { get; set; }

    public int TargetId { get; set; }
    public Target Target { get; set; }

    public string Text { get; set; }

    public int Duration { get; set; }

    public int AlertId { get; set; }
    public Alert Alert { get; set; }

    public int Size { get; set; }
}
