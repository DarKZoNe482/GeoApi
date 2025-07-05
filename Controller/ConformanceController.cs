using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("conformance")]
public class ConformanceController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            conformsTo = new[]
            {
                "http://www.opengis.net/spec/ogcapi-features-1/1.0/conf/core",
                "http://www.opengis.net/spec/ogcapi-features-1/1.0/conf/geojson"
            }
        });
    }
}