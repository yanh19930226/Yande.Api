using Sample.BulkConsole.Entities;
using ShardingCore.Core.EntityMetadatas;
using ShardingCore.Sharding.PaginationConfigurations;
using ShardingCore.VirtualRoutes.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.BulkConsole
{
    public class OrderVirtualRoute : AbstractSimpleShardingDayKeyDateTimeVirtualTableRoute<Order>
    {

        public override DateTime GetBeginTime()
        {
            return DateTime.Now.Date.AddDays(-3);
        }
        /// <summary>
        /// CreatePaginationConfiguration
        /// </summary>
        /// <returns></returns>
        public override IPaginationConfiguration<Order> CreatePaginationConfiguration()
        {
            return new OrderPaginationConfiguration();
        }

        public override void Configure(EntityMetadataTableBuilder<Order> builder)
        {

        }

        public override bool AutoCreateTableByTime()
        {
            return true;
        }
    }
}
