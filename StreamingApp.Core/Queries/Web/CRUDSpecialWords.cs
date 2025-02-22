using AutoMapper;
using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.Core.Queries.Web;
public class CRUDSpecialWords : ICRUDSpecialWords
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public CRUDSpecialWords(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public List<SpecialWordDto> GetAll()
    {
        List<SpecialWords> specialWords = _unitOfWork.SpecialWords.ToList();

        return specialWords.Select(_mapper.Map<SpecialWordDto>).ToList();
    }

    public List<SpecialWordDto> CreateOrUpdtateAll(List<SpecialWordDto> specialWords)
    {
        List<SpecialWords> allSpecialWords = _unitOfWork.SpecialWords.ToList();

        foreach (SpecialWordDto specialWord in specialWords)
        {
            var foundSpecialWord = allSpecialWords.FirstOrDefault(t => t.Id == specialWord.Id);

            if (foundSpecialWord != null)
            {
                var combinedData = _mapper.Map(foundSpecialWord, _mapper.Map<SpecialWords>(specialWord));

                _unitOfWork.Update(combinedData);
            }
            else
            {
                var mappedData = _mapper.Map<SpecialWords>(specialWord);

                _unitOfWork.Add(mappedData);
                allSpecialWords.Add(mappedData);
            }
        }

        _unitOfWork.SaveChanges();

        return allSpecialWords.Select(specialWord => _mapper.Map<SpecialWordDto>(specialWord)).ToList();
    }

    public bool Delete(List<SpecialWordDto> specialWords)
    {
        try
        {
            foreach (SpecialWordDto specialWord in specialWords)
            {
                var removeData = _unitOfWork.CommandAndResponse.FirstOrDefault(t => t.Id == specialWord.Id);

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
