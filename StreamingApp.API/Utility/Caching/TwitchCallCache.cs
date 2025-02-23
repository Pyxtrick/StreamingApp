using StreamingApp.API.Utility.Caching.CacheData;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.API.Utility.Caching;
public class TwitchCallCache : ITwitchCallCache
{

    private readonly TwitchCallCacheData _twitchCallCacheData;

    public TwitchCallCache(TwitchCallCacheData twitchCallCacheData)
    {
        _twitchCallCacheData = twitchCallCacheData;
    }

    public void AddMessage(Object message, CallCacheEnum callCacheEnum)
    {
        switch (callCacheEnum)
        {
            case CallCacheEnum.CachedMessageData:
                var messageDto = (MessageDto)message;
                if (messageDto != null)
                {
                    Console.WriteLine($"message date{messageDto.Date}");
                    _twitchCallCacheData.CachedMessageData.Add(messageDto);
                }
                break;
            case CallCacheEnum.CachedSubData:
                var subDto = (SubDto)message;
                if (subDto != null)
                {
                    _twitchCallCacheData.CachedSubData.Add(subDto);
                }
                break;
            case CallCacheEnum.CachedRaidData:
                var raidDto = (RaidDto)message;
                if (raidDto != null)
                {
                    _twitchCallCacheData.CachedRaidData.Add(raidDto);
                }
                break;
            case CallCacheEnum.CachedUserFollowData:
                var joinDto = (FollowDto)message;
                if (joinDto != null)
                {
                    _twitchCallCacheData.CachedUserFollowData.Add(joinDto);
                }
                break;
            case CallCacheEnum.CachedBannedData:
                var bannedDto = (BannedUserDto)message;
                if (bannedDto != null)
                {
                    _twitchCallCacheData.CachedBannedData.Add(bannedDto);
                }
                break;
            default:
                Console.WriteLine("Unknown data type.");
                break;
        }
    }

    public List<Object> GetAllMessages(CallCacheEnum callCacheEnum)
    {
        switch (callCacheEnum)
        {
            case CallCacheEnum.CachedMessageData:
                return _twitchCallCacheData.CachedMessageData.ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedSubData:
                return _twitchCallCacheData.CachedSubData.ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedRaidData:
                return _twitchCallCacheData.CachedRaidData.ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedUserFollowData:
                return _twitchCallCacheData.CachedUserFollowData.ConvertAll(s => (Object)s);
            default:
                Console.WriteLine("Unknown data type.");
                return new List<Object>();
        }
    }

    public List<Object> GetAllMessagesFromTo(DateTime from, DateTime to, CallCacheEnum callCacheEnum)
    {
        
        switch (callCacheEnum)
        {
            case CallCacheEnum.CachedMessageData:
                var t = _twitchCallCacheData.CachedMessageData.Where(t => t.Date >= from && t.Date <= to).ToList();
                if (t.Count != 0)
                {
                    Console.WriteLine(t.First().UserName);
                }
                return t.ConvertAll(s => (Object)s);
            /**
            case CallCacheEnum.CachedSubData:
                return (IList<Object>)_twitchCallCacheData.CachedSubData.Where(t => t.Date >= from && t.Date <= to).ToList();
            case CallCacheEnum.CachedRaidData:
                return (IList<Object>)_twitchCallCacheData.CachedRaidData.Where(t => t.Date >= from && t.Date <= to).ToList();
            case CallCacheEnum.CachedUserJoinData:
                return (IList<Object>)_twitchCallCacheData.CachedUserJoinData.Where(t => t.Date >= from && t.Date <= to).ToList();
            case CallCacheEnum.CachedBannedData:
                return (IList<Object>)_twitchCallCacheData.CachedBannedData.Where(t => t.Date >= from && t.Date <= to).ToList();
            **/
            default:
                Console.WriteLine("Unknown data type.");
                return new List<Object>();
        }
        //return new List<Object>() { new MessageDto() };
        return new List<Object>();
    }

    public void RemoveMessages(IList<Object> messages, CallCacheEnum callCacheEnum)
    {
        foreach (var message in messages)
        {
            switch (callCacheEnum)
            {
                case CallCacheEnum.CachedMessageData:
                    var messageDto = (MessageDto)message;
                    if (messageDto != null)
                    {
                        _twitchCallCacheData.CachedMessageData.Remove(messageDto);
                    }
                    break;
                case CallCacheEnum.CachedSubData:
                    var subDto = (SubDto)message;
                    if (subDto != null)
                    {
                        _twitchCallCacheData.CachedSubData.Remove(subDto);
                    }
                    break;
                case CallCacheEnum.CachedRaidData:
                    var raidDto = (RaidDto)message;
                    if (raidDto != null)
                    {
                        _twitchCallCacheData.CachedRaidData.Remove(raidDto);
                    }
                    break;
                case CallCacheEnum.CachedUserFollowData:
                    var joinDto = (FollowDto)message;
                    if (joinDto != null)
                    {
                        _twitchCallCacheData.CachedUserFollowData.Add(joinDto);
                    }
                    break;
                case CallCacheEnum.CachedBannedData:
                    var bannedUserDto = (BannedUserDto)message;
                    if (bannedUserDto != null)
                    {
                        _twitchCallCacheData.CachedBannedData.Add(bannedUserDto);
                    }
                    break;
                default:
                    Console.WriteLine("Unknown data type.");
                    break;
            }
        }
    }

    public void RemoveOlderThan(DateTime to, CallCacheEnum callCacheEnum)
    {
        switch (callCacheEnum)
        {
            case CallCacheEnum.CachedMessageData:
                var messageDto = _twitchCallCacheData.CachedMessageData.Where(t => t.Date < to).ToList();

                foreach (var message in messageDto)
                {
                    _twitchCallCacheData.CachedMessageData.Remove(message);
                }
                break;
            case CallCacheEnum.CachedSubData:
                var subDto = _twitchCallCacheData.CachedSubData.Where(t => t.Date < to).ToList();

                foreach (var message in subDto)
                {
                    _twitchCallCacheData.CachedSubData.Remove(message);
                }
                break;
            case CallCacheEnum.CachedRaidData:
                var raidDto = _twitchCallCacheData.CachedRaidData.Where(t => t.Date < to).ToList();

                foreach (var message in raidDto)
                {
                    _twitchCallCacheData.CachedRaidData.Remove(message);
                }
                break;
            case CallCacheEnum.CachedUserFollowData:
                var joinDto = _twitchCallCacheData.CachedUserFollowData.Where(t => t.Date < to).ToList();

                foreach (var message in joinDto)
                {
                    _twitchCallCacheData.CachedUserFollowData.Add(message);
                }
                break;
            case CallCacheEnum.CachedBannedData:
                var bannedUserDtos = _twitchCallCacheData.CachedBannedData.Where(t => t.Date < to).ToList();

                foreach (var message in bannedUserDtos)
                {
                    _twitchCallCacheData.CachedBannedData.Add(message);
                }
                break;
            default:
                Console.WriteLine("Unknown data type.");
                break;
        }
    }
}
