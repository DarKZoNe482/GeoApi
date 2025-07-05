public interface IToGeoJsonFeature<TSelf>
    where TSelf : IToGeoJsonFeature<TSelf>
{
    static abstract GeoJsonFeature ToFeature(TSelf entity);
}