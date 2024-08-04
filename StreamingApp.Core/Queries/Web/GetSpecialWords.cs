using AutoMapper;
using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web;
public class GetSpecialWords : IGetSpecialWords
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public GetSpecialWords(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public List<SpecialWordDto> GetAll()
    {
        List<SpecialWords> specialWords = _unitOfWork.SpecialWords.ToList();

        return specialWords.Select(specialWord => _mapper.Map<SpecialWordDto>(specialWord)).ToList();
    }
}
