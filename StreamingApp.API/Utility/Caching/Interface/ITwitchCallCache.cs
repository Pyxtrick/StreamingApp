using StreamingApp.Domain.Enums;

namespace StreamingApp.API.Utility.Caching.Interface;
public interface ITwitchCallCache
{
    void AddMessage(Object message, CallCacheEnum callCacheEnum);
    void UpdateIsUsed(List<Object> data, CallCacheEnum callCacheEnum);
    int GetChachedNumberCount(CallCacheEnum callCacheEnum);
    void ReseetCounts();
    List<Object> GetAllMessages(CallCacheEnum callCacheEnum, bool updateIsUsed);
    List<Object> GetAllUnusedMessages(CallCacheEnum callCacheEnum);
    List<Object> GetAllMessagesFromTo(DateTime from, DateTime to, CallCacheEnum callCacheEnum);
    void RemoveMessages(IList<Object> messages, CallCacheEnum callCacheEnum);
    void RemoveOlderThan(DateTime to, CallCacheEnum callCacheEnum);
}