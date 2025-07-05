using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")]
public class RootController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        return Ok(new
        {
            title = "Mon API OGC",
            description = "API conforme à OGC API - Features",
            links = new[]
            {
                new {
                    href = $"{baseUrl}/collections",
                    rel = "data",
                    type = "application/json",
                    title = "Liste des collections"
                },
                new {
                    href = $"{baseUrl}/conformance",
                    rel = "conformance",
                    type = "application/json",
                    title = "Conformance OGC"
                },
                new {
                    href = $"{baseUrl}/swagger/v1/swagger.json",
                    rel = "service-desc",
                    type = "application/vnd.oai.openapi+json;version=3.0",
                    title = "Spécification OpenAPI"
                }
            }
        });
    }
}