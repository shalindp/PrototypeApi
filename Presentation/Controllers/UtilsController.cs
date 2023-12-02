using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UtilsController : ControllerBase
{
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [HttpGet(nameof(Health))]
    public IActionResult Health()
    {
        return new OkObjectResult(new { Server = "Ok" });
    }
}