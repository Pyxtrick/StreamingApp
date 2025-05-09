using StreamingApp.Domain.Entities;

namespace StreamingApp.API.TTS;
public interface ITTSApiRequest
{
    Task SendMessage(TTSData ttsData);
}