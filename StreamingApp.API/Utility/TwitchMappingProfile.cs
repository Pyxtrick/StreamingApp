﻿using AutoMapper;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;
using TwitchLib.Client.Models;

namespace StreamingApp.API.Utility;
public class TwitchMappingProfile : Profile
{
    public TwitchMappingProfile()
    {
        CreateMap<ChatMessage, MessageDto>()
            .ConstructUsing(x => new MessageDto(
                x.Id,
                false,
                x.Channel,
                x.UserId,
                x.DisplayName,
                x.ColorHex,
                x.ChatReply != null ? x.ChatReply.ParentMsgBody : "",
                x.Message,
                x.EmoteReplacedMessage,
                x.CustomRewardId,
                x.Bits,
                MappEmotes(x),
                x.Badges,
                ChatOriginEnum.Twtich,
                MappAuth(x),
                MappSpecialMessage(x),
                EffectEnum.none,
                x.IsSubscriber,
                x.SubscribedMonthCount,
                DateTime.UtcNow
                ));

        CreateMap<ChatMessage, MessageAlertDto>()
            .ConstructUsing(x => new MessageAlertDto(
                x.Id,
                x.Channel,
                x.UserId,
                x.DisplayName,
                x.ColorHex,
                x.Message,
                x.EmoteReplacedMessage,
                x.CustomRewardId,
                x.Bits,
                MappEmotes(x),
                x.Badges,
                ChatOriginEnum.Twtich,
                AlertTypeEnum.Undefined,
                MappAuth(x),
                x.IsSubscriber,
                DateTime.UtcNow
            ));
    }

    private List<Domain.Entities.Dtos.Twitch.EmoteSet> MappEmotes(ChatMessage chatMessage)
    {
        return new(from emote in chatMessage.EmoteSet.Emotes
                   select new Domain.Entities.Dtos.Twitch.EmoteSet()
                   {
                       Name = emote.Name,
                       AnimatedURL = $"https://static-cdn.jtvnw.net/emoticons/v2/{emote.Id}/animated/light/4.0",
                       StaticURL = $"https://static-cdn.jtvnw.net/emoticons/v2/{emote.Id}/static/light/4.0"
                   });
    }

    private List<AuthEnum> MappAuth(ChatMessage chatMessage)
    {
        return new List<AuthEnum>()
        {
            chatMessage.IsBroadcaster ? AuthEnum.Streamer : AuthEnum.Undefined,
            chatMessage.IsModerator ? AuthEnum.Mod : AuthEnum.Undefined,
            chatMessage.IsStaff ? AuthEnum.Staff : AuthEnum.Undefined,
            chatMessage.IsVip ? AuthEnum.Vip : AuthEnum.Undefined,
            chatMessage.IsSubscriber ? AuthEnum.Subscriber : AuthEnum.Undefined,
            chatMessage.IsTurbo ? AuthEnum.Turbo : AuthEnum.Undefined,
            chatMessage.IsPartner ? AuthEnum.Partner : AuthEnum.Undefined,
        }.Where(a => a != 0).ToList();
    }

    private List<SpecialMessgeEnum> MappSpecialMessage(ChatMessage chatMessage)
    {
        return new List<SpecialMessgeEnum>()
        {
            chatMessage.IsFirstMessage ? SpecialMessgeEnum.FirstMessage : 0,
            chatMessage.IsHighlighted ? SpecialMessgeEnum.Highlighted : 0,
            chatMessage.IsSkippingSubMode ? SpecialMessgeEnum.SkippSubMode : 0,
        }.Where(a => a != 0).ToList();
    }
}
