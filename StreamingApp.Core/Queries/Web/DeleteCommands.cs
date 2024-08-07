﻿using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web;
public class DeleteCommands : IDeleteCommands
{
    private readonly UnitOfWorkContext _unitOfWork;

    public DeleteCommands(UnitOfWorkContext unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool Delete(List<CommandAndResponseDto> commands)
    {
        try
        {
            foreach (CommandAndResponseDto command in commands)
            {
                var removeData = _unitOfWork.CommandAndResponse.FirstOrDefault(t => t.Id == command.Id);

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
