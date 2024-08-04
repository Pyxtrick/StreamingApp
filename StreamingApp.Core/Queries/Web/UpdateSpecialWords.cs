using AutoMapper;
using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web;
public class UpdateSpecialWords : IUpdateSpecialWords
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public UpdateSpecialWords(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
}
