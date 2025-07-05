using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;

[ApiController]
[Route("collections/zones")]
public class ZoneController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCollectionMetadata()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        return Ok(new
        {
            id = "zones",
            title = "Zones Inondables",
            description = "Contours géographiques des zones inondables du Québec",
            itemType = "feature",
            extent = new
            {
                spatial = new
                {
                    bbox = new[] { new[] { -79.75, 44.9, -57.0, 62.0 } }, // ajuster selon tes données
                    crs = "http://www.opengis.net/def/crs/OGC/1.3/CRS84"
                }
            },
            crs = new[]
            {
                "http://www.opengis.net/def/crs/OGC/1.3/CRS84"
            },
            links = new[]
            {
                new {
                    href = $"{baseUrl}/collections/zones/items",
                    rel = "items",
                    type = "application/geo+json",
                    title = "Entités de la collection 'zones'"
                },
                new {
                    href = $"{baseUrl}/collections/zones",
                    rel = "self",
                    type = "application/json",
                    title = "Métadonnées de la collection 'zones'"
                }
            }
        });
    }

    [HttpOptions]
    public IActionResult Options() => NoContent();

    [HttpGet("items")]
    public IActionResult GetZones()
    {
        var zones = new List<ZoneInondable>
        {
            new()
            {
                Id = 1,
                Nom = "Zone A",
                Contour = new MultiPolygon(
                [
                    new Polygon(new LinearRing(
                [
                    new Coordinate(-75.5, 45.5),
                    new Coordinate(-75.5, 46.0),
                    new Coordinate(-74.5, 46.0),
                    new Coordinate(-74.5, 45.5),
                    new Coordinate(-75.5, 45.5)
                ]))
                ])
            }
        };

        var features = zones.Select(ZoneInondable.ToFeature).ToList();
        return Ok(new
        {
            type = "FeatureCollection",
            features = features
        });
    }

    [HttpOptions("items")]
    public IActionResult OptionsForItems()
    {
        return NoContent();
    }
}

