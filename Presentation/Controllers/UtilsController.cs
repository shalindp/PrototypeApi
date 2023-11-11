using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UtilsController : ControllerBase
{
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [HttpGet(nameof(Ping))]
    public IActionResult Ping()
    {
        return new OkObjectResult(new { Server = "Ok" });
    }
}