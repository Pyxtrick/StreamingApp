using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.Core.Commands.DB.CRUD;
public class CRUDSpecialWords : ICRUDSpecialWords
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public CRUDSpecialWords(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SpecialWordDto>> GetAll()
    {
        List<SpecialWords> specialWords = _unitOfWork.SpecialWords.ToList();

        return specialWords.Select(_mapper.Map<SpecialWordDto>).ToList();
    }

    public async Task<List<SpecialWordDto>> CreateOrUpdtateAll(List<SpecialWordDto> specialWords)
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

                await _unitOfWork.AddAsync(mappedData);
                allSpecialWords.Add(mappedData);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        return allSpecialWords.Select(specialWord => _mapper.Map<SpecialWordDto>(specialWord)).ToList();
    }

    public async Task<bool> Delete(List<SpecialWordDto> specialWords)
    {
        try
        {
            foreach (SpecialWordDto specialWord in specialWords)
            {
                var removeData = await _unitOfWork.CommandAndResponse.FirstOrDefaultAsync(t => t.Id == specialWord.Id);

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
