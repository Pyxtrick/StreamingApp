using StreamingApp.Core.VTubeStudio.Cache.CacheData;
using StreamingApp.Core.VTubeStudio.Cache.Interface;
using StreamingApp.Domain.Entities.VtubeStudio;

namespace StreamingApp.API.Utility.Caching;

public class VtubeStudioCache : IVtubeStudioCache
{
    private readonly VtubeStudioCacheData _vtubeStudioCacheData;

    public VtubeStudioCache(VtubeStudioCacheData emotesCacheData)
    {
        _vtubeStudioCacheData = emotesCacheData;
    }

    public void AddAllData(VtubeStudioData vtubeStudioData)
    {
        if (vtubeStudioData.AvailableItems != null) _vtubeStudioCacheData.AvailableItems = vtubeStudioData.AvailableItems;

        if (vtubeStudioData.ItemsInScene != null) _vtubeStudioCacheData.ItemsInScene = vtubeStudioData.ItemsInScene;


        if (vtubeStudioData.Models != null) {
            if (vtubeStudioData.Models.Count > 1)
            {
                _vtubeStudioCacheData.Models = vtubeStudioData.Models;
            }
            else
            {
                var indexOld = _vtubeStudioCacheData.Models.FindIndex(m => m.IsActive);
                var indexNew = _vtubeStudioCacheData.Models.FindIndex(m => m.ModelID == vtubeStudioData.Models[0].ModelID);

                _vtubeStudioCacheData.Models[indexOld].IsActive = false;
                _vtubeStudioCacheData.Models[indexNew].IsActive = true;
            }
        }

        if (vtubeStudioData.ModelToggles != null) _vtubeStudioCacheData.ModelToggles = vtubeStudioData.ModelToggles;
    }

    public VtubeStudioData AllData()
    {
        return new()
        {
            AvailableItems = _vtubeStudioCacheData.AvailableItems,
            ItemsInScene = _vtubeStudioCacheData.ItemsInScene,
            Models = _vtubeStudioCacheData.Models,
            ModelToggles = _vtubeStudioCacheData.ModelToggles,
        };
    }

    public List<Item> GetItems()
    {
        return _vtubeStudioCacheData.AvailableItems;
    }

    public List<Model> GetModels()
    {
        return _vtubeStudioCacheData.Models;
    }

    public List<Toggle> GetToggles()
    {
        return _vtubeStudioCacheData.ModelToggles;
    }
}
