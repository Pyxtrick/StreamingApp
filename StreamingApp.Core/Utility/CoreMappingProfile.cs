using StreamingApp.Domain.Entities.Internal;
using AutoMapper;
using StreamingApp.Domain.Entities.Internal.Trigger;
using StreamingApp.Domain.Entities.Dtos;
using Stream = StreamingApp.Domain.Entities.Internal.Stream.Stream;

namespace StreamingApp.Core.Utility;
public class CoreMappingProfile : Profile
{
    public CoreMappingProfile()
    {
        CreateMap<CommandAndResponse, CommandAndResponseDto>().ReverseMap();
        CreateMap<CommandAndResponse, CommandAndResponse>().ForAllMembers(opts => opts.Condition((src, dest, member) => member != null));

        CreateMap<SpecialWords, SpecialWordDto>().ReverseMap();
        CreateMap<SpecialWords, SpecialWords>().ForAllMembers(opts => opts.Condition((src, dest, member) => member != null));

        CreateMap<Stream, StreamDto>().ReverseMap();
        CreateMap<Stream, Stream>().ForAllMembers(opts => opts.Condition((src, dest, member) => member != null));
    }
}
