using AutoMapper;
using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web;
public class GetCommands : IGetCommands
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public GetCommands(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public List<CommandAndResponseDto> GetAll()
    {
        List<CommandAndResponse> commands = _unitOfWork.CommandAndResponse.ToList();

        return commands.Select(command => _mapper.Map<CommandAndResponseDto>(command)).ToList();
    }
}
