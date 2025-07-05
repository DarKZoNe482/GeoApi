using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class ApiLandingPageController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Redirect("/swagger/v1/swagger.json");
    }
}
