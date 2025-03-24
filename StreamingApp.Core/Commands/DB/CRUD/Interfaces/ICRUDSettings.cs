using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Commands.DB.CRUD.Interfaces;
public interface ICRUDSettings
{
    Task<List<SettingsDto>> GetAll();
    Task<bool> Update(SettingsDto newSettings);
}