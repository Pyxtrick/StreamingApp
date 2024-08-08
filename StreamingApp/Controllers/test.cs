using CvTool.Api.Core.Authorizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthorizationPolicies.Admin)]
public class test : ControllerBase
{

    [HttpGet]
    public IActionResult Index()
    {
        var t = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        return null;
    }
}
