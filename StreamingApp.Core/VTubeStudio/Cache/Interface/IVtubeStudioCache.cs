using StreamingApp.Domain.Entities.VtubeStudio;

namespace StreamingApp.Core.VTubeStudio.Cache.Interface;
public interface IVtubeStudioCache
{
    void AddAllData(VtubeStudioData vtubeStudioData);
    VtubeStudioData AllData();

    List<Item> GetItems();

    List<Model> GetModels();

    List<Toggle> GetToggles();
}