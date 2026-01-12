using StreamingApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.Dtos;
public class SettingsDto
{
    public int Id { get; set; }

    public OriginEnum Origin { get; set; }

    public bool MuteAllerts { get; set; }

    public bool PauseAllerts { get; set; }

    public bool IsAdsDisplay { get; set; }

    public int AllertDelayS { get; set; }
}
