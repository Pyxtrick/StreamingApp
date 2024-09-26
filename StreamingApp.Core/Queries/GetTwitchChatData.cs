﻿using AutoMapper;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Queries;
public class GetTwitchChatData : IGetTwitchChatData
{
    private readonly ITwitchCallCache _twitchCallCache;

    private readonly IMapper _mapper;

    public GetTwitchChatData(ITwitchCallCache twitchCallCache, IMapper mapper)
    {
        _twitchCallCache = twitchCallCache;
        _mapper = mapper;
    }

    public List<ChatDto> Execute()
    {
        List<MessageDto>? t = _twitchCallCache.GetAllMessages(CallCacheEnum.CachedMessageData) as List<MessageDto>;

        List<ChatDto> chatDtos = _mapper.Map<List<ChatDto>>(t);

        return chatDtos;
    }
}