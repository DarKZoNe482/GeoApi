using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("collections")]
public class CollectionsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCollections()
    {
        return Ok(new
        {
            collections = new[]
            {
                new
                {
                    name = "zones",
                    title = "Zones Inondables",
                    description = "Contours des zones inondables",
                    extent = new
                    {
                        spatial = new
                        {
                            bbox = new[] { new[] { -79.75, 44.9, -57.0, 62.0 } },
                            crs = "http://www.opengis.net/def/crs/OGC/1.3/CRS84"
                        }
                    },
                    links = new[]
                    {
                        new {
                            href = "/collections/zones/items",
                            rel = "items",
                            type = "application/geo+json",
                            title = "Liste des zones"
                        }
                    }
                }
            }
        });
    }
}