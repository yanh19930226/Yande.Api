using Sample.BulkConsole.Entities;
using ShardingCore.Sharding.PaginationConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.BulkConsole
{
    public class OrderPaginationConfiguration : IPaginationConfiguration<Order>
    {
        public void Configure(PaginationBuilder<Order> builder)
        {
            builder.PaginationSequence(o => o.CreateTime)
                .UseRouteComparer(Comparer<string>.Default)
                .UseQueryMatch(PaginationMatchEnum.Owner | PaginationMatchEnum.Named | PaginationMatchEnum.PrimaryMatch).UseAppendIfOrderNone();
            builder.ConfigReverseShardingPage(0.5d, 10000L);
        }
    }
}
