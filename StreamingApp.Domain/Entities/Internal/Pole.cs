namespace StreamingApp.Domain.Entities.Internal;

public class Pole
{
    public int Id { get; set; }
    public string PoleId { get; set; }

    public bool IsPole { get; set; }

    public string Title { get; set; }

    public List<Choice> Choices { get; set; }

    public DateTime StartedAt { get; set; }
}
