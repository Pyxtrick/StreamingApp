using StreamingApp.API.Utility.Caching.CacheData;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;
using System.Linq;

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
                    _twitchCallCacheData.CachedMessageData.Add(messageDto);
                }
                break;
            case CallCacheEnum.CachedCommandData:
                var commandDto = (CommandDto)message;
                if (commandDto != null)
                {
                    _twitchCallCacheData.CachedCommandData.Add(commandDto);
                }
                break;
            case CallCacheEnum.CachedGiftedSubData:
                var subDto = (SubDto)message;
                if (subDto != null)
                {
                    _twitchCallCacheData.CachedGiftedSubData.Add(subDto);
                }
                break;
            case CallCacheEnum.CachedNewSubData:

                subDto = (SubDto)message;
                if (subDto != null)
                {
                    _twitchCallCacheData.CachedNewSubData.Add(subDto);
                }
                break;
            case CallCacheEnum.CachedPrimeSubData:
                subDto = (SubDto)message;
                if (subDto != null)
                {
                    _twitchCallCacheData.CachedPrimeSubData.Add(subDto);
                }
                break;
            case CallCacheEnum.CachedReSubData:
                subDto = (SubDto)message;
                if (subDto != null)
                {
                    _twitchCallCacheData.CachedReSubData.Add(subDto);
                }
                break;
            case CallCacheEnum.CachedRaidData:
                var raidDto = (RaidDto)message;
                if (raidDto != null)
                {
                    _twitchCallCacheData.CachedRaidData.Add(raidDto);
                }
                break;
            case CallCacheEnum.CachedUserJoinData:
                var joinDto = (JoinDto)message;
                if (joinDto != null)
                {
                    _twitchCallCacheData.CachedUserJoinData.Add(joinDto);
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
                var t = _twitchCallCacheData.CachedMessageData.ConvertAll(s => (Object)s);
                return t;
            case CallCacheEnum.CachedCommandData:
                return _twitchCallCacheData.CachedCommandData.ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedGiftedSubData:
                return _twitchCallCacheData.CachedGiftedSubData.ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedNewSubData:
                return _twitchCallCacheData.CachedNewSubData.ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedPrimeSubData:
                return _twitchCallCacheData.CachedPrimeSubData.ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedReSubData:
                return _twitchCallCacheData.CachedReSubData.ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedRaidData:
                return _twitchCallCacheData.CachedRaidData.ConvertAll(s => (Object)s);
            case CallCacheEnum.CachedUserJoinData:
                return _twitchCallCacheData.CachedUserJoinData.ConvertAll(s => (Object)s);
            default:
                Console.WriteLine("Unknown data type.");
                return new List<Object>();
        }
    }

    public IList<Object> GetAllMessagesFromTo(DateTime from, DateTime to, CallCacheEnum callCacheEnum)
    {
        /**
        switch (callCacheEnum)
        {
            case CallCacheEnum.CachedMessageData:
                return (IList<Object>)_twitchCallCacheData.CachedMessageData.Where(t => t.Date >= from && t.Date <= to).ToList();
            case CallCacheEnum.CachedCommandData:
                return (IList<Object>)_twitchCallCacheData.CachedCommandData.Where(t => t.Date >= from && t.Date <= to).ToList();
            case CallCacheEnum.CachedGiftedSubData:
                return (IList<Object>)_twitchCallCacheData.CachedGiftedSubData.Where(t => t.Date >= from && t.Date <= to).ToList();
            case CallCacheEnum.CachedNewSubData:
                return (IList<Object>)_twitchCallCacheData.CachedNewSubData.Where(t => t.Date >= from && t.Date <= to).ToList();
            case CallCacheEnum.CachedPrimeSubData:
                return (IList<Object>)_twitchCallCacheData.CachedPrimeSubData.Where(t => t.Date >= from && t.Date <= to).ToList();
            case CallCacheEnum.CachedReSubData:
                return (IList<Object>)_twitchCallCacheData.CachedReSubData.Where(t => t.Date >= from && t.Date <= to).ToList();
            case CallCacheEnum.CachedRaidData:
                return (IList<Object>)_twitchCallCacheData.CachedRaidData.Where(t => t.Date >= from && t.Date <= to).ToList();
            case CallCacheEnum.CachedUserJoinData:
                return (IList<Object>)_twitchCallCacheData.CachedUserJoinData.Where(t => t.Date >= from && t.Date <= to).ToList();
            default:
                Console.WriteLine("Unknown data type.");
                return new List<Object>();
        }
        **/
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
                case CallCacheEnum.CachedCommandData:
                    var commandDto = (CommandDto)message;
                    if (commandDto != null)
                    {
                        _twitchCallCacheData.CachedCommandData.Remove(commandDto);
                    }
                    break;
                case CallCacheEnum.CachedGiftedSubData:
                    var subDto = (SubDto)message;
                    if (subDto != null)
                    {
                        _twitchCallCacheData.CachedGiftedSubData.Remove(subDto);
                    }
                    break;
                case CallCacheEnum.CachedNewSubData:

                    subDto = (SubDto)message;
                    if (subDto != null)
                    {
                        _twitchCallCacheData.CachedNewSubData.Remove(subDto);
                    }
                    break;
                case CallCacheEnum.CachedPrimeSubData:
                    subDto = (SubDto)message;
                    if (subDto != null)
                    {
                        _twitchCallCacheData.CachedPrimeSubData.Remove(subDto);
                    }
                    break;
                case CallCacheEnum.CachedReSubData:
                    subDto = (SubDto)message;
                    if (subDto != null)
                    {
                        _twitchCallCacheData.CachedReSubData.Remove(subDto);
                    }
                    break;
                case CallCacheEnum.CachedRaidData:
                    var raidDto = (RaidDto)message;
                    if (raidDto != null)
                    {
                        _twitchCallCacheData.CachedRaidData.Remove(raidDto);
                    }
                    break;
                case CallCacheEnum.CachedUserJoinData:
                    var joinDto = (JoinDto)message;
                    if (joinDto != null)
                    {
                        _twitchCallCacheData.CachedUserJoinData.Add(joinDto);
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
            case CallCacheEnum.CachedCommandData:
                var commandDto = _twitchCallCacheData.CachedCommandData.Where(t => t.Date < to).ToList();

                foreach (var message in commandDto)
                {
                    _twitchCallCacheData.CachedCommandData.Remove(message);
                }
                break;
            case CallCacheEnum.CachedGiftedSubData:
                var subDto = _twitchCallCacheData.CachedGiftedSubData.Where(t => t.Date < to).ToList();

                foreach (var message in subDto)
                {
                    _twitchCallCacheData.CachedGiftedSubData.Remove(message);
                }
                break;
            case CallCacheEnum.CachedNewSubData:
                subDto = _twitchCallCacheData.CachedNewSubData.Where(t => t.Date < to).ToList();

                foreach (var message in subDto)
                {
                    _twitchCallCacheData.CachedNewSubData.Remove(message);
                }
                break;
            case CallCacheEnum.CachedPrimeSubData:
                subDto = _twitchCallCacheData.CachedPrimeSubData.Where(t => t.Date < to).ToList();

                foreach (var message in subDto)
                {
                    _twitchCallCacheData.CachedPrimeSubData.Remove(message);
                }
                break;
            case CallCacheEnum.CachedReSubData:
                subDto = _twitchCallCacheData.CachedReSubData.Where(t => t.Date < to).ToList();

                foreach (var message in subDto)
                {
                    _twitchCallCacheData.CachedReSubData.Remove(message);
                }
                break;
            case CallCacheEnum.CachedRaidData:
                var raidDto = _twitchCallCacheData.CachedRaidData.Where(t => t.Date < to).ToList();

                foreach (var message in raidDto)
                {
                    _twitchCallCacheData.CachedRaidData.Remove(message);
                }
                break;
            case CallCacheEnum.CachedUserJoinData:
                var joinDto = _twitchCallCacheData.CachedUserJoinData.Where(t => t.Date < to).ToList();

                foreach (var message in joinDto)
                {
                    _twitchCallCacheData.CachedUserJoinData.Add(message);
                }
                break;
            default:
                Console.WriteLine("Unknown data type.");
                break;
        }
    }
}
