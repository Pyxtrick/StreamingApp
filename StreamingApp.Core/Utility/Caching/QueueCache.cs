using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Utility.Caching.CacheData;
using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.API.Utility.Caching;

public class QueueCache : IQueueCache
{
    private readonly QueueData _chatQueueData;
    public QueueCache(QueueData chatQueueData)
    {
        _chatQueueData = chatQueueData;
    }

    /// <summary>
    /// User can join a Queue
    /// </summary>
    /// <param name="userQueueDto"></param>
    /// <returns></returns>
    public int AddUserToQueue(UserQueueDto userQueueDto)
    {
        var foundUser = _chatQueueData.CachedQueueData.First(t => t == userQueueDto);

        var queue = _chatQueueData.CachedQueueData.Last().Queue;

        userQueueDto.Queue = queue++;

        if (foundUser == null)
        {
            _chatQueueData.CachedQueueData.Add(userQueueDto);
        }

        return userQueueDto.Queue;
    }

    /// <summary>
    /// Removes User out of Queue
    /// </summary>
    /// <param name="userName"></param>
    public bool RemoveQueueUser(string userName)
    {
        var foundUser = _chatQueueData.CachedQueueData.First(t => t.UserName == userName && t.IsActive == true);
        if (foundUser != null)
        {
            _chatQueueData.CachedQueueData.Remove(foundUser);
            moveQueueNumber();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get Current user from the Queue
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public IList<UserQueueDto> GetQueueUsers(int number)
    {
        int count = 1;

        IList<UserQueueDto> newUserQueues = new List<UserQueueDto>();

        foreach (UserQueueDto userQueue in _chatQueueData.CachedQueueData)
        {
            if (userQueue.Queue == count && count <= number)
            {
                count++;
                newUserQueues.Add(userQueue);
            }
        }

        return newUserQueues;
    }

    /// <summary>
    /// Removes Current User form the Queue
    /// </summary>
    /// <param name="number"></param>
    public void RemoveCurrentQueue(int number)
    {
        IList<UserQueueDto> userQueues = _chatQueueData.CachedQueueData;

        int t = 0;

        foreach (UserQueueDto userQueue in userQueues)
        {
            if (userQueue.IsActive == true)
            {
                t++;
                if (t == number)
                {
                    userQueue.IsActive = false;
                }
            }
        }
        moveQueueNumber();
    }

    /// <summary>
    /// Moves user to last in Queue
    /// </summary>
    /// <param name="userName"></param>
    public bool MoveUserQueue(string userName)
    {
        var foundUser = _chatQueueData.CachedQueueData.First(t => t.UserName == userName && t.IsActive == true);
        if (foundUser != null)
        {
            _chatQueueData.CachedQueueData.Remove(foundUser);
            _chatQueueData.CachedQueueData.Add(foundUser);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Moves Queue to The next People
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public IList<UserQueueDto> MoveQueue(int number)
    {
        IList<UserQueueDto> userQueues = _chatQueueData.CachedQueueData;

        int count = 0;

        IList<UserQueueDto> newUserQueues = new List<UserQueueDto>();

        foreach (UserQueueDto userQueue in userQueues)
        {
            if (userQueue.IsActive == true)
            {
                if (count == number)
                {
                    return newUserQueues;
                }
                count++;

                newUserQueues.Add(userQueue);
            }
        }

        moveQueueNumber();

        return newUserQueues;
    }

    /// <summary>
    /// Get User Queue Possition
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public int GetQueuePosition(string userName)
    {
        return _chatQueueData.CachedQueueData.First(t => t.UserName == userName).Queue;
    }

    /// <summary>
    /// Cooses a random user from the list
    /// </summary>
    /// <returns></returns>
    public UserQueueDto ChooseRandomFromQueue()
    {
        IList<UserQueueDto> userQueues = _chatQueueData.CachedQueueData;

        userQueues = userQueues.Where(t => t.IsActive).ToList();

        var randomUser = userQueues[new Random().Next(userQueues.Count)];

        randomUser.IsActive = false;

        _chatQueueData.CachedQueueData.Add(randomUser);

        return randomUser;
    }

    /// <summary>
    /// return count of active users
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int GetQueueCount()
    {
        return _chatQueueData.CachedQueueData.Where(t => t.IsActive).Count();
    }

    private void moveQueueNumber()
    {
        int queueNumber = 1;

        foreach (var userQueue in _chatQueueData.CachedQueueData)
        {
            if (userQueue.IsActive == false)
            {
                userQueue.Queue = 0;
            }
            if (userQueue.IsActive == true)
            {
                userQueue.Queue = queueNumber;
                queueNumber++;
            }
        }
    }
}
