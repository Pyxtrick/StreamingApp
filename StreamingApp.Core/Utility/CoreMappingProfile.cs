using StreamingApp.Domain.Entities.Internal;
using AutoMapper;
using StreamingApp.Domain.Entities.Internal.Trigger;
using StreamingApp.Domain.Entities.Dtos;
using Stream = StreamingApp.Domain.Entities.Internal.Stream.Stream;
using StreamingApp.Domain.Entities.Internal.Stream;

namespace StreamingApp.Core.Utility;
public class CoreMappingProfile : Profile
{
    public CoreMappingProfile()
    {
        CreateMap<CommandAndResponse, CommandAndResponseDto>().ReverseMap();
        CreateMap<CommandAndResponse, CommandAndResponse>().ForAllMembers(opts => opts.Condition((src, dest, member) => member != null));

        CreateMap<SpecialWords, SpecialWordDto>().ReverseMap();
        CreateMap<SpecialWords, SpecialWords>().ForAllMembers(opts => opts.Condition((src, dest, member) => member != null));

        
        CreateMap<Stream, StreamDto>()
            .ForMember(dest => dest.GameHistoryDtos, opt => opt.MapFrom(src => src.GameCategories))
            .ReverseMap();
        CreateMap<Stream, Stream>().ForAllMembers(opts => opts.Condition((src, dest, member) => member != null));
                
        CreateMap<StreamGame, GameHistoryDto>()
            .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.GameCategory.Game))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.GameCategory.Message))
            .ForMember(dest => dest.GameCategory, opt => opt.MapFrom(src => src.GameCategory.GameCategory))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate)).ReverseMap();
        CreateMap<StreamGame, StreamGame>().ForAllMembers(opts => opts.Condition((src, dest, member) => member != null));
        
        CreateMap<GameInfo, GameInfoDto>().ReverseMap();
        CreateMap<GameInfo, GameInfo>().ForAllMembers(opts => opts.Condition((src, dest, member) => member != null));
    }
}
