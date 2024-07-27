using AutoMapper;
using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web;
public class UpdateCommands : IUpdateCommands
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public UpdateCommands(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public List<CommandAndResponseDto> UpdtateAll(List<CommandAndResponseDto> commands)
    {
        List<CommandAndResponse> foundCommands = _unitOfWork.CommandAndResponse.ToList();

        return commands.Select(command => _mapper.Map<CommandAndResponseDto>(command)).ToList();
    }
}
