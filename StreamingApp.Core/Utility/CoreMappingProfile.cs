using StreamingApp.Domain.Entities.Internal;
using AutoMapper;

namespace StreamingApp.Core.Utility;
public class CoreMappingProfile : Profile
{
    public CoreMappingProfile()
    {
        CreateMap<CommandAndResponse, CommandAndResponseDto>().ReverseMap();
        CreateMap<CommandAndResponse, CommandAndResponse>().ForAllMembers(opts => opts.Condition((src, dest, member) => member != null));

        CreateMap<SpecialWords, SpecialWordDto>().ReverseMap();
        CreateMap<SpecialWords, SpecialWords>().ForAllMembers(opts => opts.Condition((src, dest, member) => member != null));
    }
}
