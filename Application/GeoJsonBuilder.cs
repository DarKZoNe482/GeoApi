public static class GeoJsonBuilder
{
    public static GeoJsonFeatureCollection BuildFeatureCollection<T>(IEnumerable<T> entities)
        where T : IToGeoJsonFeature<T>
    {
        return new GeoJsonFeatureCollection
        {
            Features = entities.Select(T.ToFeature).ToList()
        };
    }
}