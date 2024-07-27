using AutoMapper;
using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web;
public class DeleteCommands : IDeleteCommands
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public DeleteCommands(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public bool Delete(List<CommandAndResponseDto> commands)
    {
        List<CommandAndResponse> foundCommands = _unitOfWork.CommandAndResponse.ToList();

        return true;
    }
}
