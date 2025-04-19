using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Domain.Entities.InternalDB;

public class SettingsRespose
{
    public List<SettingsDto> Settings { get; set; }

    public bool isSucsess { get; set; }
}
