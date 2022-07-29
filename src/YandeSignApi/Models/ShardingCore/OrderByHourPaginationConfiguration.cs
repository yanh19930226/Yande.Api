using ShardingCore.Sharding.PaginationConfigurations;
using System.Collections.Generic;

namespace YandeSignApi.Models.ShardingCore
{
    public class OrderByHourPaginationConfiguration : IPaginationConfiguration<OrderByHour>
    {
        public void Configure(PaginationBuilder<OrderByHour> builder)
        {
            builder.PaginationSequence(o => o.CreateTime)
                .UseRouteComparer(Comparer<string>.Default)
                .UseQueryMatch(PaginationMatchEnum.Owner | PaginationMatchEnum.Named | PaginationMatchEnum.PrimaryMatch).UseAppendIfOrderNone();
            builder.ConfigReverseShardingPage(0.5d, 10000L);
        }
    }
}
