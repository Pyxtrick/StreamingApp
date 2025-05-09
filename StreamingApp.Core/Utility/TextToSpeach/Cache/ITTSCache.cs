using StreamingApp.Domain.Entities;

namespace StreamingApp.Core.Utility.TextToSpeach.Cache;
public interface ITTSCache
{
    void AddTTSData(TTSData data);
    List<TTSData> GetAllTTSData();
    TTSData GetLatestTTSData();
    TTSData? GetSpecificTTSData(int id);
    int GetTTSCount();
}