using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.VtubeStudio;

namespace StreamingApp.Core.VTubeStudio.Cache.CacheData;

public class VtubeStudioCacheData
{
    public List<Toggle> ModelToggles { get; set; } = new List<Toggle>();

    public List<Model> Models { get; set; } = new List<Model>();

    public List<Item> AvailableItems { get; set; } = new List<Item>();

    public List<Item> ItemsInScene { get; set; } = new List<Item>();

}
