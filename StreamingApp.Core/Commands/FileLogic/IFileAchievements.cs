
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.FileLogic;

public interface IFileAchievements
{
    Task Execute(OriginEnum platform, string user, string what, int count, string contence);
}