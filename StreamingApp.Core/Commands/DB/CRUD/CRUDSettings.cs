using AutoMapper;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.InternalDB.Settings;

namespace StreamingApp.Core.Commands.DB.CRUD;

public class CRUDSettings : ICRUDSettings
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public CRUDSettings(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SettingsDto>> GetAll()
    {
        List<Settings> setting = _unitOfWork.Settings.ToList();

        return setting.Select(_mapper.Map<SettingsDto>).ToList();
    }

    public async Task<bool> Update(SettingsDto newSettings)
    {
        try
        {
            Settings oldSettings = _unitOfWork.Settings.FirstOrDefault(s => s.Id == newSettings.Id);

            var settings = _mapper.Map(newSettings, oldSettings);

            _unitOfWork.Update(settings);

            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }
}
