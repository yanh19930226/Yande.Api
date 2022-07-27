using Microsoft.EntityFrameworkCore;
using ShardingCore.Core.VirtualRoutes.TableRoutes.RouteTails.Abstractions;
using ShardingCore.Sharding;
using ShardingCore.Sharding.Abstractions;
using YandeSignApi.Models.ShardingCore;

namespace YandeSignApi.Data
{
    public class DefaultDbContext : AbstractShardingDbContext, IShardingTableDbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new OrderByHourMap());
        }

        public IRouteTail RouteTail { get; set; }
    }
}
