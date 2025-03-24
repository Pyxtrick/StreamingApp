using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.Core.Commands.DB.CRUD;
public class CRUDCommands : ICRUDCommands
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public CRUDCommands(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CommandAndResponseDto>> GetAll()
    {
        List<CommandAndResponse> commands = _unitOfWork.CommandAndResponse.ToList();

        return commands.Select(_mapper.Map<CommandAndResponseDto>).ToList();
    }

    public async Task<List<CommandAndResponseDto>> CreateOrUpdtateAll(List<CommandAndResponseDto> commands)
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

                await _unitOfWork.AddAsync(mappedData);
                allCommands.Add(mappedData);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        return allCommands.Select(_mapper.Map<CommandAndResponseDto>).ToList();
    }

    public async Task<bool> Delete(List<CommandAndResponseDto> commands)
    {
        try
        {
            foreach (CommandAndResponseDto command in commands)
            {
                var removeData = await _unitOfWork.CommandAndResponse.FirstOrDefaultAsync(t => t.Id == command.Id);

                if (removeData != null)
                {
                    _unitOfWork.Remove(removeData);
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
