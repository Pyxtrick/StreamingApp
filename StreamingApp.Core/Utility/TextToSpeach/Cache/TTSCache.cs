using StreamingApp.Core.Utility.TextToSpeach.Cache.CacheData;
using StreamingApp.Domain.Entities;

namespace StreamingApp.Core.Utility.TextToSpeach.Cache;

public class TTSCache : ITTSCache
{
    private readonly TTSCacheData _ttsCacheData;

    public TTSCache(TTSCacheData ttsCacheData)
    {
        _ttsCacheData = ttsCacheData;
    }

    public void AddTTSData(TTSData data)
    {
        data.Id = _ttsCacheData.TTSData.Count() + 1;

        _ttsCacheData.TTSData.Add(data);
    }

    public TTSData GetLatestTTSData()
    {
        var tts = _ttsCacheData.TTSData.Where(t => t.IsActive).OrderBy(t => t.Posted);
        tts.First().IsActive = false;

        return tts.First();
    }

    public TTSData? GetSpecificTTSData(int id)
    {
        var t = _ttsCacheData.TTSData.FirstOrDefault(t => t.Id == id);

        t.IsActive = false;

        return _ttsCacheData.TTSData.FirstOrDefault(t => t.Id == id) ?? null;
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
