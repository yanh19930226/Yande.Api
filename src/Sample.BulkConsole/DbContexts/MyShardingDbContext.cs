using Microsoft.EntityFrameworkCore;
using Sample.BulkConsole.Entities;
using ShardingCore.Core.VirtualRoutes.TableRoutes.RouteTails.Abstractions;
using ShardingCore.Sharding;
using ShardingCore.Sharding.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.BulkConsole.DbContexts
{
    public class MyShardingDbContext : AbstractShardingDbContext, IShardingTableDbContext
    {
        public MyShardingDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.OrderNo).IsRequired().HasMaxLength(128).IsUnicode(false);
                entity.ToTable(nameof(Order));
            });
        }

        public IRouteTail RouteTail { get; set; }
    }
}
