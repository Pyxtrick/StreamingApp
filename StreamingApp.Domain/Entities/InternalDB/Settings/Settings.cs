using StreamingApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.InternalDB.Settings;

public class Settings
{
    public int Id { get; set; }

    // Applies settings for specific platform
    public OriginEnum Origin { get; set; }

    // Changes who is shown in the the "OnScreen" chat
    [Required]
    public AuthEnum AllChat { get; set; }

    // Mutes Allets in the frontend
    public bool MuteAllerts { get; set; }

    public bool PauseAllerts { get; set; }

    // Pauses for Send Messages in Chat
    public bool PauseChatMessages { get; set; }

    // To activate comunity Day commands
    public bool ComunityDayActive { get; set; }

    // TODO:  
    //public bool SubathonActive { get; set; }

    // TODO: every x minutes: https://crontab.cronhub.io/
    // Sends a message every x minutes with out been pined | for sponsor or anouncements
    [Required]
    public string Delay { get; set; }

    // Allert Delays in Seconds
    public int AllertDelayS { get; set; }

    // How long a user will be timeouted at minimum if a banned word has been used
    public int TimeOutSeconds { get; set; }

    // Stops TTS if the same message is used X times
    public int TTSSpamAmmount { get; set; }

    // Stops TTS if the message is longer than X Words
    public int TTSLenghtAmmount { get; set; }
}
