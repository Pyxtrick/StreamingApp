using AutoMapper;
using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.Core.Queries.Web;
public class CRUDCommands : ICRUDCommands
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public CRUDCommands(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public List<CommandAndResponseDto> GetAll()
    {
        List<CommandAndResponse> commands = _unitOfWork.CommandAndResponse.ToList();

        return commands.Select(_mapper.Map<CommandAndResponseDto>).ToList();
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

        return allCommands.Select(_mapper.Map<CommandAndResponseDto>).ToList();
    }

    public bool Delete(List<CommandAndResponseDto> commands)
    {
        try
        {
            foreach (CommandAndResponseDto command in commands)
            {
                var removeData = _unitOfWork.CommandAndResponse.FirstOrDefault(t => t.Id == command.Id);

                if (removeData != null)
                {
                    _unitOfWork.Remove(removeData);
                }
            }

            _unitOfWork.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
