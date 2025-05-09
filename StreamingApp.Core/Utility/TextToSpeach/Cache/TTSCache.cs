using StreamingApp.Core.Utility.TextToSpeach.Cache.CacheData;
using StreamingApp.Domain.Entities;

namespace StreamingApp.Core.Utility.TextToSpeach.Cache;

public class TTSCache : ITTSCache
{
    private readonly TTSCacheData _ttsCacheData;


    public void AddTTSData(TTSData data)
    {
        _ttsCacheData.TTSData.Add(data);
    }

    public TTSData GetLatestTTSData()
    {
        var tts = _ttsCacheData.TTSData.Where(t => t.IsActive).OrderBy(t => t.Posted);

        return tts.First();
    }

    public List<TTSData> GetAllTTSData()
    {
        return _ttsCacheData.TTSData.OrderBy(t => t.Posted).ToList();
    }

    public int GetTTSCount()
    {
        return _ttsCacheData.TTSData?.Count ?? 0;
    }
}
