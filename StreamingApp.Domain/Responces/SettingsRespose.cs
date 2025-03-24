using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Domain.Entities.Internal;

public class SettingsRespose
{
    public List<SettingsDto> Settings { get; set; }

    public bool isSucsess { get; set; }
}
