using AutoMapper;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.InternalDB.Settings;
using StreamingApp.Domain.Enums;

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
        List<Settings> settings = _unitOfWork.Settings.ToList();

        return settings.Select(_mapper.Map<SettingsDto>).ToList();
    }

    public async Task<SettingsDto> GetSettingByOrigin(OriginEnum origin)
    {
        Settings setting = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == origin);

        return _mapper.Map<SettingsDto>(setting);
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

    public async Task<bool> SwitchData(SettingsEnum setting, bool data)
    {
        try
        {
            Settings oldSettings = _unitOfWork.Settings.FirstOrDefault(s => s.Origin == OriginEnum.Twitch);

            switch (setting)
            {
                case SettingsEnum.adsDisplay:
                    oldSettings.IsAdsDisplay = data;
                    break;
                case SettingsEnum.pauseAllert:
                    oldSettings.PauseAllerts = data;
                    break;
                case SettingsEnum.muteAllert:
                    oldSettings.MuteAllerts = data;
                    break;
                case SettingsEnum.useGameName:
                    oldSettings.UseGameName = data;
                    break;
                default: break;
            }

            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }
}
