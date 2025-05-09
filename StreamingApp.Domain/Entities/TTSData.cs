namespace StreamingApp.Domain.Entities;

public class TTSData
{
    public string Message { get; set; }

    public string OriginalMessage { get; set; }

    public int MessageLengthSeconds { get; set; }

    public bool IsActive { get; set; }

    public DateTime Posted { get; set; }
}
