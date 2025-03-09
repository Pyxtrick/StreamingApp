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
                    _twitchCallCacheData.CachedMessageNumber++;
                }
                if (_twitchCallCacheData.CachedMessageData.Count > 1000)
                {
                    _twitchCallCacheData.CachedMessageData.RemoveRange(0, 10);
                }
                break;
            case CallCacheEnum.CachedSubData:
                var subDto = (SubDto)message;
                if (subDto != null)
                {
                    _twitchCallCacheData.CachedSubData.Add(subDto);
                    _twitchCallCacheData.CachedSubNumber += subDto.GifftedSubCount > 0 ? subDto.GifftedSubCount : 1;
                }
                if(_twitchCallCacheData.CachedSubData.Count > 100)
                {
                    _twitchCallCacheData.CachedSubData.RemoveRange(0, 10);
                }
                break;
            case CallCacheEnum.CachedAlertData:
                var alertDto = (MessageAlertDto)message;
                if(alertDto != null)
                {
                    _twitchCallCacheData.CachedAlertData.Add(alertDto);
                }
                if (_twitchCallCacheData.CachedAlertData.Count > 100)
                {
                    _twitchCallCacheData.CachedAlertData.RemoveRange(0, 10);
                }
                break;
            case CallCacheEnum.CachedRaidData:
                var raidDto = (RaidDto)message;
                if (raidDto != null)
                {
                    _twitchCallCacheData.CachedRaidData.Add(raidDto);
                    _twitchCallCacheData.CachedRaidNumber++;
                    _twitchCallCacheData.CachedRaidNumber += raidDto.Count;
                }
                if (_twitchCallCacheData.CachedRaidData.Count > 100)
                {
                    _twitchCallCacheData.CachedRaidData.RemoveRange(0, 10);
                }
                break;
            case CallCacheEnum.CachedUserFollowData:
                var joinDto = (FollowDto)message;
                if (joinDto != null)
                {
                    _twitchCallCacheData.CachedUserFollowData.Add(joinDto);
                    _twitchCallCacheData.CachedUserFollowNumber++;
                }
                if (_twitchCallCacheData.CachedUserFollowData.Count > 100)
                {
                    _twitchCallCacheData.CachedUserFollowData.RemoveRange(0, 10);
                }
                break;
            case CallCacheEnum.CachedBannedData:
                var bannedDto = (BannedUserDto)message;
                if (bannedDto != null)
                {
                    _twitchCallCacheData.CachedBannedData.Add(bannedDto);
                    _twitchCallCacheData.CachedBannedNumber++;
                }
                if (_twitchCallCacheData.CachedBannedData.Count > 100)
                {
                    _twitchCallCacheData.CachedBannedData.RemoveRange(0, 10);
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
            case CallCacheEnum.CachedAlertData:
                return _twitchCallCacheData.CachedAlertData.ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedRaidData:
                return _twitchCallCacheData.CachedRaidData.ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedUserFollowData:
                return _twitchCallCacheData.CachedUserFollowData.ConvertAll(s => (Object)s);
            default:
                Console.WriteLine("Unknown data type.");
                return new List<Object>();
        }
    }

    public int GetChachedNumberCount(CallCacheEnum callCacheEnum)
    {
        switch (callCacheEnum)
        {
            case CallCacheEnum.CachedMessageData:
                return _twitchCallCacheData.CachedMessageNumber;
            case CallCacheEnum.CachedSubData:
                return _twitchCallCacheData.CachedSubNumber;
            case CallCacheEnum.CachedRaidData:
                return _twitchCallCacheData.CachedRaidNumber;
            case CallCacheEnum.CachedRaidUserData:
                return _twitchCallCacheData.CachedRaidUserNumber;
            case CallCacheEnum.CachedUserFollowData:
                return _twitchCallCacheData.CachedUserFollowNumber;
            default:
                Console.WriteLine("Unknown data type.");
                return 0;
        }
    }

    public void ReseetCounts()
    {
        _twitchCallCacheData.CachedMessageNumber = 0;
        _twitchCallCacheData.CachedSubNumber = 0;
        _twitchCallCacheData.CachedRaidNumber = 0;
        _twitchCallCacheData.CachedRaidUserNumber = 0;
        _twitchCallCacheData.CachedUserFollowNumber = 0;
    }

    public List<Object> GetAllMessagesFromTo(DateTime from, DateTime to, CallCacheEnum callCacheEnum)
    {
        
        switch (callCacheEnum)
        {
            case CallCacheEnum.CachedMessageData:
                return _twitchCallCacheData.CachedMessageData.Where(t => t.Date >= from && t.Date <= to).ToList().ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedSubData:
                return _twitchCallCacheData.CachedSubData.Where(t => t.Date >= from && t.Date <= to).ToList().ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedAlertData:
                return _twitchCallCacheData.CachedRaidData.Where(t => t.Date >= from && t.Date <= to).ToList().ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedRaidData:
                return _twitchCallCacheData.CachedRaidData.Where(t => t.Date >= from && t.Date <= to).ToList().ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedBannedData:
                return _twitchCallCacheData.CachedBannedData.Where(t => t.Date >= from && t.Date <= to).ToList().ConvertAll(s => (Object)s);
            default:
                Console.WriteLine("Unknown data type.");
                return new List<Object>();
        }
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
