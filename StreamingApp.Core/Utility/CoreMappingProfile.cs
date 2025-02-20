using StreamingApp.Domain.Entities.Internal;
using AutoMapper;
using StreamingApp.Domain.Entities.Internal.Trigger;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Utility;
public class CoreMappingProfile : Profile
{
    public CoreMappingProfile()
    {
        CreateMap<CommandAndResponse, CommandAndResponseDto>().ReverseMap();
        CreateMap<CommandAndResponse, CommandAndResponse>().ForAllMembers(opts => opts.Condition((src, dest, member) => member != null));

        CreateMap<MessageDto, ChatDto>().ConstructUsing(x => new ChatDto(
            x.MessageId,
            x.UserName,
            x.ColorHex,
            x.ReplayMessage,
            x.Message,
            x.EmoteReplacedMessage,
            x.Emotes,
            x.Badges,
            x.ChatOrigin,
            Domain.Enums.ChatDisplayEnum.twitchChat,
            x.Auth,
            x.SpecialMessage,
            x.Effect,
            x.Date
            ));

        CreateMap<SpecialWords, SpecialWordDto>().ReverseMap();
        CreateMap<SpecialWords, SpecialWords>().ForAllMembers(opts => opts.Condition((src, dest, member) => member != null));
    }
}
