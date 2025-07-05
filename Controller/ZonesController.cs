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
public IActionResult GetZones([FromQuery] int limit = 10, [FromQuery] int offset = 0)
{
    // On génère des zones fictives dans le sud du Québec, entre -76 et -66 de longitude
    var random = new Random();
var allZones = Enumerable.Range(0, 50).Select(i =>
{
    // Boîte approximative du Québec
    double minLon = -79.5;
    double maxLon = -57.0;
    double minLat = 45.0;
    double maxLat = 61.0;

    // Taille de chaque polygone (en degrés)
    double width = 1.0;
    double height = 0.7;

    // Génération pseudo-aléatoire dans la zone
    double baseLon = minLon + (maxLon - minLon - width) * random.NextDouble();
    double baseLat = minLat + (maxLat - minLat - height) * random.NextDouble();

    return new ZoneInondable
    {
        Id = i + 1,
        Nom = $"Zone {i + 1}",
        Sqrc = $"Q{100 + i / 10:D3}-{i % 10 + 1:D2}",
        Contour = new MultiPolygon(
        [
            new Polygon(new LinearRing(
            [
                new Coordinate(baseLon, baseLat),
                new Coordinate(baseLon, baseLat + height),
                new Coordinate(baseLon + width, baseLat + height),
                new Coordinate(baseLon + width, baseLat),
                new Coordinate(baseLon, baseLat)
            ]))
        ])
    };
}).ToList();


    var paginated = allZones.Skip(offset).Take(limit).ToList();
    var features = paginated.Select(ZoneInondable.ToFeature).ToList();

    var baseUrl = $"{Request.Scheme}://{Request.Host}/collections/zones/items";
    var links = new List<object>
    {
        new {
            href = $"{baseUrl}?limit={limit}&offset={offset}",
            rel = "self",
            type = "application/geo+json",
            title = "Page courante"
        }
    };

    if (offset + limit < allZones.Count)
    {
        links.Add(new {
            href = $"{baseUrl}?limit={limit}&offset={offset + limit}",
            rel = "next",
            type = "application/geo+json",
            title = "Page suivante"
        });
    }

    return Ok(new
    {
        type = "FeatureCollection",
        features = features,
        numberReturned = features.Count,
        numberMatched = allZones.Count,
        links = links
    });
}


    [HttpOptions("items")]
    public IActionResult OptionsForItems()
    {
        return NoContent();
    }
}

