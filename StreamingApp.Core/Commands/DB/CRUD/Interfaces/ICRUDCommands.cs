﻿using StreamingApp.Domain.Entities.InternalDB;

namespace StreamingApp.Core.Commands.DB.CRUD.Interfaces;
public interface ICRUDCommands
{
    Task<List<CommandAndResponseDto>> GetAll();

    Task<List<CommandAndResponseDto>> CreateOrUpdtateAll(List<CommandAndResponseDto> commands);

    Task<bool> Delete(List<CommandAndResponseDto> commands);
}