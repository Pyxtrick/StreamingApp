using AutoMapper;
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
                false,
                x.Channel,
                x.HexColor,
                x.ChatReply != null ? x.ChatReply.ParentMsgBody : "",
                x.Message,
                x.EmoteReplacedMessage,
                MappEmotes(x.EmoteSet),
                x.Badges,
                OriginEnum.Twitch,
                MappAuth(x),
                MappSpecialMessage(x),
                EffectEnum.none,
                x.UserDetail.IsSubscriber,
                x.SubscribedMonthCount,
                false,
                x.Id,
                x.UserId,
                x.Username,
                x.DisplayName,
                DateTime.UtcNow
            ));

        CreateMap<ChatMessage, MessageAlertDto>()
            .ConstructUsing(x => new MessageAlertDto(
                x.Id,
                x.Channel,
                x.UserId,
                x.Username,
                x.DisplayName,
                x.HexColor,
                x.Message,
                x.EmoteReplacedMessage,
                x.CustomRewardId,
                x.Bits,
                0,
                MappEmotes(x.EmoteSet),
                x.Badges,
                OriginEnum.Twitch,
                AlertTypeEnum.Undefined,
                MappAuth(x),
                x.UserDetail.IsSubscriber,
                false,
                DateTime.UtcNow
            ));


        CreateMap<Subscriber, SubDto>()
            .ConstructUsing(x => new SubDto(
                x.MsgId,
                x.UserId,
                x.DisplayName,
                x.DisplayName,
                x.RoomId,
                OriginEnum.Twitch,
                false,
                0,
                x.MsgParamCumulativeMonths,
                (TierEnum)Enum.Parse(typeof(TierEnum), x.MsgParamSubPlan.ToString()),
                null,
                false,
                DateTime.UtcNow
            ));

        CreateMap<PrimePaidSubscriber, SubDto>()
            .ConstructUsing(x => new SubDto(
                x.MsgId,
                x.UserId,
                x.DisplayName,
                x.DisplayName,
                x.RoomId,
                OriginEnum.Twitch,
                false,
                0,
                0,//x.MsgParamCumulativeMonths,
                (TierEnum)Enum.Parse(typeof(TierEnum), x.MsgParamSubPlan.ToString()),
                null,
                false,
                DateTime.UtcNow
            ));

        CreateMap<ReSubscriber, SubDto>()
            .ConstructUsing(x => new SubDto(
                x.MsgId,
                x.UserId,
                x.DisplayName,
                x.DisplayName,
                x.RoomId,
                OriginEnum.Twitch,
                false,
                0,
                x.MsgParamCumulativeMonths,
                (TierEnum)Enum.Parse(typeof(TierEnum), x.MsgParamSubPlan.ToString()),
                new MessageDto(
                    false,
                    x.RoomId,
                    x.UserId,
                    x.DisplayName,
                    x.DisplayName,
                    x.HexColor,
                    new List<EmoteSetDto>(),
                    x.Badges,
                    OriginEnum.Twitch,
                    new List<AuthEnum>() { AuthEnum.Subscriber },
                    new List<SpecialMessgeEnum>(),
                    EffectEnum.none,
                    true,
                    x.MsgParamCumulativeMonths,
                    false,
                    x.Id,
                    null,
                    x.ResubMessage,
                    x.Emotes,
                    DateTime.UtcNow
                ),
                false,
                DateTime.UtcNow
            ));
    }

    private static List<EmoteSetDto> MappEmotes(EmoteSet chatMessage)
    {
        return new(from emote in chatMessage.Emotes
                   select new EmoteSetDto()
                   {
                       Name = emote.Name,
                       AnimatedURL = $"https://static-cdn.jtvnw.net/emoticons/v2/{emote.Id}/animated/light/4.0",
                       StaticURL = $"https://static-cdn.jtvnw.net/emoticons/v2/{emote.Id}/static/light/4.0"
                   });
    }

    private static List<AuthEnum> MappAuth(ChatMessage chatMessage)
    {
        return new List<AuthEnum>()
        {
            //chatMessage.UserDetail.is

            chatMessage.IsBroadcaster ? AuthEnum.Streamer : AuthEnum.Undefined,
            chatMessage.UserDetail.IsModerator ? AuthEnum.Mod : AuthEnum.Undefined,
            chatMessage.UserDetail.IsStaff ? AuthEnum.Staff : AuthEnum.Undefined,
            chatMessage.UserDetail.IsVip ? AuthEnum.Vip : AuthEnum.Undefined,
            chatMessage.UserDetail.IsSubscriber ? AuthEnum.Subscriber : AuthEnum.Undefined,
            //chatMessage.UserDetail.IsTurbo ? AuthEnum.Turbo : AuthEnum.Undefined,
            chatMessage.UserDetail.IsPartner ? AuthEnum.Partner : AuthEnum.Undefined,
        }.Where(a => a != 0).ToList();
    }

    private static List<SpecialMessgeEnum> MappSpecialMessage(ChatMessage chatMessage)
    {
        return new List<SpecialMessgeEnum>()
        {
            chatMessage.IsFirstMessage ? SpecialMessgeEnum.FirstMessage : 0,
            chatMessage.IsHighlighted ? SpecialMessgeEnum.Highlighted : 0,
            chatMessage.IsSkippingSubMode ? SpecialMessgeEnum.SkippSubMode : 0,
        }.Where(a => a != 0).ToList();
    }
}
