public class GeoJsonFeature
{
    public string Type => "Feature";
    public object Geometry { get; set; }
    public Dictionary<string, object> Properties { get; set; } = new();
}

public class GeoJsonFeatureCollection
{
    public string Type => "FeatureCollection";
    public List<GeoJsonFeature> Features { get; set; } = new();
}