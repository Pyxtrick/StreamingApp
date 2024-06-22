using StreamingApp.Domain.Entities.Internal;
using AutoMapper;

namespace StreamingApp.Core.Utility;
public class CoreMappingProfile : Profile
{
    public CoreMappingProfile()
    {
        CreateMap<CommandAndResponse, CommandAndResponseDto>().ReverseMap();
    }
}
