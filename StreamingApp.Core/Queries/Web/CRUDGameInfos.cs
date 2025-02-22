using AutoMapper;
using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Internal.Stream;
namespace StreamingApp.Core.Queries.Web;

public class CRUDGameInfos : ICRUDGameInfos
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public CRUDGameInfos(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public List<GameInfoDto> GetAll()
    {
        List<GameInfo> gameInfos = _unitOfWork.GameInfo.ToList();

        return gameInfos.Select(_mapper.Map<GameInfoDto>).ToList();
    }

    public List<GameInfoDto> CreateOrUpdtateAll(List<GameInfoDto> commands)
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

                _unitOfWork.Add(mappedData);
                allGameInfos.Add(mappedData);
            }
        }

        _unitOfWork.SaveChanges();

        return allGameInfos.Select(_mapper.Map<GameInfoDto>).ToList();
    }

    public bool Delete(List<GameInfoDto> gameInfos)
    {
        try
        {
            foreach (GameInfoDto gameInfo in gameInfos)
            {
                var removeData = _unitOfWork.GameInfo.FirstOrDefault(t => t.Id == gameInfo.Id);

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
