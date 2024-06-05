namespace StreamingApp.Domain.Entities.Dtos;

public class EmoteDto
{
    public EmoteDto(string id, string name, string url)
    {
        Id = id;
        Name = name;
        Url = url;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}
