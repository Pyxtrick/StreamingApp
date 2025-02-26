using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos;

public class EmoteDto
{
    public EmoteDto(string id, EmoteProviderEnum emoteProviderEnum, string name, string url)
    {
        Id = id;
        Provider = emoteProviderEnum;
        Name = name;
        Url = url;
    }

    public string Id { get; set; }
    public EmoteProviderEnum Provider { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}
