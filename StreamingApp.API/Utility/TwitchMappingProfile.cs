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
            .ForMember(dest => dest.IsCommand, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.DisplayName))
            .ForMember(dest => dest.PointRediam, opt => opt.MapFrom(src => src.CustomRewardId))
            .ForMember(dest => dest.ChatOrigin, opt => opt.MapFrom(src => ChatOriginEnum.Twtich))
            .ForMember(dest => dest.Auth, opt => opt.MapFrom(src => MappAuth(src)))
            .ForMember(dest => dest.SpecialMessage, opt => opt.MapFrom(src => MappMessage(src)))
            .ForMember(dest => dest.Effect, opt => opt.MapFrom(src => EffectEnum.none))
            .ForMember(dest => dest.ReplayMessage, opt => opt.MapFrom(src => ""))
            .ForMember(dest => dest.IsSub, opt => opt.MapFrom(src => src.IsSubscriber))
            .ReverseMap();
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

    private List<SpecialMessgeEnum> MappMessage(ChatMessage chatMessage)
    {
        return new List<SpecialMessgeEnum>()
        {
            chatMessage.IsFirstMessage ? SpecialMessgeEnum.FirstMessage : 0,
            chatMessage.IsHighlighted ? SpecialMessgeEnum.Highlighted : 0,
            chatMessage.IsSkippingSubMode ? SpecialMessgeEnum.SkippSubMode : 0,
        }.Where(a => a != 0).ToList();
    }
}
