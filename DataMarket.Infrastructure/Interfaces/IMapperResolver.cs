namespace DataMarket.Infrastructure
{
    public interface IMapperResolver
    {
        TDestination Map<TDestination>(object source);
    }
}
