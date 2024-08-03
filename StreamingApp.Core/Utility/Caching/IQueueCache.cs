using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.API.Utility.Caching.Interface;

public interface IQueueCache
{
    // Queue
    int AddUserToQueue(UserQueueDto userQueueDto);
    bool RemoveQueueUser(string userName);
    IList<UserQueueDto> GetQueueUsers(int number);
    void RemoveCurrentQueue(int number);
    bool MoveUserQueue(string userName);
    IList<UserQueueDto> MoveQueue(int number);
    int GetQueuePosition(string userName);
    UserQueueDto ChooseRandomFromQueue();
}