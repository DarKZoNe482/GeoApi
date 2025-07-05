using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System.Text.Json;

public class ZoneInondable : IToGeoJsonFeature<ZoneInondable>
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public MultiPolygon Contour { get; set; }

    public static GeoJsonFeature ToFeature(ZoneInondable zone)
    {
        var writer = new GeoJsonWriter();
        return new GeoJsonFeature
        {
            Geometry = JsonSerializer.Deserialize<object>(writer.Write(zone.Contour))!,
            Properties = new()
            {
                { "id", zone.Id },
                { "nom", zone.Nom }
            }
        };
    }
}