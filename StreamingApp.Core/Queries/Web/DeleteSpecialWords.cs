using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Core.Queries.Web;
public class DeleteSpecialWords : IDeleteSpecialWords
{
    private readonly UnitOfWorkContext _unitOfWork;

    public DeleteSpecialWords(UnitOfWorkContext unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
