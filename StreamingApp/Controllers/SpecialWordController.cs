using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecialWordController : ControllerBase
{
    [HttpGet]
    public SpecialWordRespose GetAllspecialWords([FromServices] IGetSpecialWords getSpecialWords)
    {
        var specialWords = getSpecialWords.GetAll();

        return new SpecialWordRespose()
        {
            sw = specialWords,
            isSucsess = specialWords.Any(),
        };
    }

    [HttpPost]
    public SpecialWordRespose UpdatespecialWords([FromServices] IUpdateSpecialWords updateSpecialWords, List<SpecialWordDto> commandAndResponses)
    {
        var specialWords = updateSpecialWords.CreateOrUpdtateAll(commandAndResponses);

        return new SpecialWordRespose()
        {
            sw = specialWords,
            isSucsess = specialWords.Any()
        };
    }

    [HttpDelete]
    public SpecialWordRespose DeletespecialWords([FromServices] IDeleteSpecialWords deleteSpecialWords, List<SpecialWordDto> commandAndResponses)
    {
        var sucsess = deleteSpecialWords.Delete(commandAndResponses);

        return new SpecialWordRespose()
        {
            isSucsess = sucsess
        };
    }
}
