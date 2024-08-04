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

    public List<CommandAndResponseDto> CreateOrUpdtateAll(List<CommandAndResponseDto> commands)
    {
        List<CommandAndResponse> allCommands = _unitOfWork.CommandAndResponse.ToList();

        foreach (CommandAndResponseDto specialWord in commands)
        {
            var foundCommands = allCommands.FirstOrDefault(t => t.Id == specialWord.Id);

            if (foundCommands != null)
            {
                var combinedData = _mapper.Map(foundCommands, _mapper.Map<CommandAndResponseDto>(specialWord));

                _unitOfWork.Update(combinedData);
            }
            else
            {
                var mappedData = _mapper.Map<CommandAndResponse>(specialWord);

                _unitOfWork.Add(mappedData);
                allCommands.Add(mappedData);
            }
        }

        _unitOfWork.SaveChanges();

        return allCommands.Select(command => _mapper.Map<CommandAndResponseDto>(command)).ToList();
    }
}
