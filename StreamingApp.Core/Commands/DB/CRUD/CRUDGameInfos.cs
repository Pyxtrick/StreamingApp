using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Internal.Stream;
namespace StreamingApp.Core.Commands.DB.CRUD;

public class CRUDGameInfos : ICRUDGameInfos
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public CRUDGameInfos(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GameInfoDto>> GetAll()
    {
        List<GameInfo> gameInfos = _unitOfWork.GameInfo.ToList();

        return gameInfos.Select(_mapper.Map<GameInfoDto>).ToList();
    }

    public async Task<List<GameInfoDto>> CreateOrUpdtateAll(List<GameInfoDto> commands)
    {
        List<GameInfo> allGameInfos = _unitOfWork.GameInfo.ToList();

        foreach (GameInfoDto specialWord in commands)
        {
            var foundGameInfo = allGameInfos.FirstOrDefault(t => t.Id == specialWord.Id);

            if (foundGameInfo != null)
            {
                var combinedData = _mapper.Map(foundGameInfo, _mapper.Map<GameInfo>(specialWord));

                _unitOfWork.Update(combinedData);
            }
            else
            {
                var mappedData = _mapper.Map<GameInfo>(specialWord);

                await _unitOfWork.AddAsync(mappedData);
                allGameInfos.Add(mappedData);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        return allGameInfos.Select(_mapper.Map<GameInfoDto>).ToList();
    }

    public async Task<bool> Delete(List<GameInfoDto> gameInfos)
    {
        try
        {
            foreach (GameInfoDto gameInfo in gameInfos)
            {
                var removeData = await _unitOfWork.GameInfo.FirstOrDefaultAsync(t => t.Id == gameInfo.Id);

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
