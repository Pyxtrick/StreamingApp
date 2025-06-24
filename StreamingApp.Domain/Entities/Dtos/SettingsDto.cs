using StreamingApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.Dtos;
public class SettingsDto
{
    public int Id { get; set; }

    public OriginEnum Origin { get; set; }

    public AuthEnum AllChat { get; set; }

    public bool MuteAllerts { get; set; }

    public bool MuteChatMessages { get; set; }

    public bool ComunityDayActive { get; set; }

    public string Delay { get; set; }

    public int AllertDelayS { get; set; }

    public int TimeOutSeconds { get; set; }

    public int SpamAmmount { get; set; }
}
