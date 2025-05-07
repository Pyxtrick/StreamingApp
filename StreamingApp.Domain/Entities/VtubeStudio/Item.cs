namespace StreamingApp.Domain.Entities.VtubeStudio;

public class Item
{
    public string FileName { get; set; }

    public string? InstanceID { get; set; }

    public float PositionX { get; set; }

    public float PositionY { get; set; }

    public float Size { get; set; }

    public float Rotation { get; set; }

    public bool Flipped { get; set; }

    public int Order { get; set; }

    public bool Censored { get; set; }
}
