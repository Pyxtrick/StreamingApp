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
                x.Id,
                false,
                x.Channel,
                x.UserId,
                x.DisplayName,
                x.ColorHex,
                "replayMessage",
                x.Message,
                x.EmoteReplacedMessage,
                "pointRediam",
                x.Bits,
                x.EmoteSet,
                x.Badges,
                ChatOriginEnum.Twtich,
                MappAuth(x),
                MappSpecialMessage(x),
                EffectEnum.none,
                x.IsSubscriber,
                x.SubscribedMonthCount,
                DateTime.UtcNow
                ));
    }

    private List<AuthEnum> MappAuth(ChatMessage chatMessage)
    {
        return new List<AuthEnum>()
        {
            chatMessage.IsBroadcaster ? AuthEnum.Streamer : AuthEnum.undefined,
            chatMessage.IsModerator ? AuthEnum.Mod : AuthEnum.undefined,
            chatMessage.IsStaff ? AuthEnum.Staff : AuthEnum.undefined,
            chatMessage.IsVip ? AuthEnum.Vip : AuthEnum.undefined,
            chatMessage.IsSubscriber ? AuthEnum.Subscriber : AuthEnum.undefined,
            chatMessage.IsTurbo ? AuthEnum.Turbo : AuthEnum.undefined,
            chatMessage.IsPartner ? AuthEnum.Partner : AuthEnum.undefined,
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
