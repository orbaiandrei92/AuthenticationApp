using AutoMapper;

namespace DataMarket.Infrastructure.Common
{
    public class AutomapperResolver : IMapperResolver
    {
        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}
