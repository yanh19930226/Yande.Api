using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RedisPublishAndSubHelper;
using Sample.BulkConsole.DbContexts;
using Sample.BulkConsole.Entities;
using ShardingCore;
using ShardingCore.Bootstrapers;
using ShardingCore.Extensions;
using ShardingCore.Extensions.ShardingPageExtensions;
using ShardingCore.TableExists;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.BulkConsole
{
    internal class Program
    {
        public static readonly ILoggerFactory efLogger = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Warning).AddConsole();
        });

        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddLogging();

            #region ShardingCore
            //services.AddShardingDbContext<MyShardingDbContext>()
            //        .AddEntityConfig(o =>
            //        {
            //            o.CreateShardingTableOnStart = true;
            //            o.EnsureCreatedWithOutShardingTable = true;
            //            o.AddShardingTableRoute<OrderVirtualRoute>();
            //        })
            //        .AddConfig(op =>
            //        {

            //            op.ConfigId = "c1";

<<<<<<< HEAD
            //            op.AddDefaultDataSource("ds0", "Server=localhost;uid=sa;pwd=sa123;Database=MyOrderSharding;MultipleActiveResultSets=true;");
            //            op.UseShardingQuery((conStr, builder) =>
            //            {
            //                builder.UseSqlServer(conStr).UseLoggerFactory(efLogger);
            //            });
            //            op.UseShardingTransaction((connection, builder) =>
            //            {
            //                builder.UseSqlServer(connection).UseLoggerFactory(efLogger);
            //            });
            //            op.ReplaceTableEnsureManager(sp => new SqlServerTableEnsureManager<MyShardingDbContext>());

            //        }).EnsureConfig();
=======

                    op.ConfigId = "c1";
                    op.AddDefaultDataSource("ds0", "server=localhost;port=3306;database=shardingTest;userid=root;password=root;AllowLoadLocalInfile=true");
                    op.UseShardingQuery((conn, b) =>
                    {
                        b.UseMySql(conn, new MySqlServerVersion(new Version())).UseLoggerFactory(efLogger);
                    });
                    op.UseShardingTransaction((conn, b) =>
                    {
                        b.UseMySql(conn, new MySqlServerVersion(new Version())).UseLoggerFactory(efLogger);
                    });
                    op.ReplaceTableEnsureManager(sp => new MySqlTableEnsureManager<MyShardingDbContext>());


                    #region SqlServer
                    //op.ConfigId = "c1";

                    //op.AddDefaultDataSource("ds0", "Server=localhost;uid=sa;pwd=sa123;Database=MyOrderSharding;MultipleActiveResultSets=true;");
                    //op.UseShardingQuery((conStr, builder) =>
                    //{
                    //    builder.UseSqlServer(conStr).UseLoggerFactory(efLogger);
                    //});
                    //op.UseShardingTransaction((connection, builder) =>
                    //{
                    //    builder.UseSqlServer(connection).UseLoggerFactory(efLogger);
                    //});
                    //op.ReplaceTableEnsureManager(sp => new SqlServerTableEnsureManager<MyShardingDbContext>()); 
                    #endregion
>>>>>>> 1f9ad8cdc0e314da4f52d81ebe82a39eefc51b9c

            //var serviceProvider = services.BuildServiceProvider();
            //serviceProvider.GetService<IShardingBootstrapper>().Start();
            //using (var serviceScope = serviceProvider.CreateScope())
            //{
            //    var myShardingDbContext = serviceScope.ServiceProvider.GetService<MyShardingDbContext>();

            //    #region 插入数据
            //    //if (!myShardingDbContext.Set<Order>().Any())
            //    //{
            //    //    var begin = DateTime.Now.Date.AddDays(-3);
            //    //    var now = DateTime.Now;
            //    //    var current = begin;
            //    //    ICollection<Order> orders = new LinkedList<Order>();
            //    //    int i = 0;
            //    //    while (current < now)
            //    //    {
            //    //        orders.Add(new Order()
            //    //        {
            //    //            Id = i.ToString(),
            //    //            OrderNo = $"orderno-" + i.ToString(),
            //    //            Seq = i,
            //    //            CreateTime = current
            //    //        });
            //    //        i++;

            //    //        Console.WriteLine($"orderno-" + i.ToString());
            //    //        current = current.AddMilliseconds(100);
            //    //    }

            //    //    var startNew = Stopwatch.StartNew();
            //    //    var bulkShardingEnumerable = myShardingDbContext.BulkShardingTableEnumerable(orders);
            //    //    startNew.Stop();
            //    //    Console.WriteLine($"订单总数:{i}条,myShardingDbContext.BulkShardingEnumerable(orders)用时:{startNew.ElapsedMilliseconds}毫秒");
            //    //    startNew.Restart();
            //    //    foreach (var dataSourceMap in bulkShardingEnumerable)
            //    //    {
            //    //        dataSourceMap.Key.BulkInsert(dataSourceMap.Value.ToList());
            //    //    }
            //    //    startNew.Stop();
            //    //    Console.WriteLine($"订单总数:{i}条,myShardingDbContext.BulkInsert(orders)用时:{startNew.ElapsedMilliseconds}毫秒");

            //    //    Console.WriteLine("ok");
            //    //} 
            //    #endregion

            //    var b = DateTime.Now.Date.AddDays(-2);
            //    var queryable = myShardingDbContext.Set<Order>().Select(o => new { Id = o.Id, OrderNo = o.OrderNo, CreateTime = o.CreateTime });//.Where(o => o.CreateTime >= b);

            //    var startNew1 = Stopwatch.StartNew();
            //    int skip = 0, take = 1000;
            //    for (int i = 20; i < 30000; i++)
            //    {
            //        skip = take * i;
            //        startNew1.Restart();
            //        var shardingPagedResult = queryable.ToShardingPage(i + 1, take);
            //        startNew1.Stop();
            //        Console.WriteLine($"流式分页skip:[{skip}],take:[{take}]耗时用时:{startNew1.ElapsedMilliseconds}毫秒");
            //    }

<<<<<<< HEAD
            //    Console.WriteLine("ok");

            //} 
            #endregion
=======
                //    Console.WriteLine("ok");
                //}
                #endregion


                var b = DateTime.Now.Date.AddDays(-3);
                var queryable = myShardingDbContext.Set<Order>().Select(o => new { Id = o.Id, OrderNo = o.OrderNo, CreateTime = o.CreateTime });//.Where(o => o.CreateTime >= b);
                var startNew1 = Stopwatch.StartNew();
                startNew1.Restart();
                var list2 = queryable.Take(1000).ToList();
                startNew1.Stop();
                Console.WriteLine($"获取1000条用时:{startNew1.ElapsedMilliseconds}毫秒");

                startNew1.Restart();
                var list = queryable.Take(10).ToList();
                startNew1.Stop();
                Console.WriteLine($"获取10条用时:{startNew1.ElapsedMilliseconds}毫秒");


                startNew1.Restart();
                var list1 = queryable.Take(100).ToList();
                startNew1.Stop();
                Console.WriteLine($"获取100条用时:{startNew1.ElapsedMilliseconds}毫秒");


                startNew1.Restart();
                var list3 = queryable.Take(10000).ToList();
                startNew1.Stop();
                Console.WriteLine($"获取100000条用时:{startNew1.ElapsedMilliseconds}毫秒");



                startNew1.Restart();
                var list4 = queryable.Take(20000).ToList();
                startNew1.Stop();
                Console.WriteLine($"获取20000条用时:{startNew1.ElapsedMilliseconds}毫秒");

                //var b = DateTime.Now.Date.AddDays(-3);
                //var queryable = myShardingDbContext.Set<Order>().Select(o => new { Id = o.Id, OrderNo = o.OrderNo, CreateTime = o.CreateTime });//.Where(o => o.CreateTime >= b);

                //var startNew1 = Stopwatch.StartNew();
                //int skip = 0, take = 1000;
                //for (int i = 20; i < 30000; i++)
                //{
                //    skip = take * i;
                //    startNew1.Restart();
                //    var shardingPagedResult = queryable.ToShardingPage(i + 1, take);
                //    startNew1.Stop();
                //    Console.WriteLine($"流式分页skip:[{skip}],take:[{take}]耗时用时:{startNew1.ElapsedMilliseconds}毫秒");
                //}

                Console.WriteLine("ok");

            }
>>>>>>> 1f9ad8cdc0e314da4f52d81ebe82a39eefc51b9c

            Console.WriteLine("Hello World!");

            #region 入队的code
            {
                int Index = 100000;
                while (Index > 0)
                {
                    //string msg = Console.ReadLine();
                    new MyRedisSubPublishHelper().PublishMessage("nihaofengge", $"你好风哥：Guid值是：{DateTime.Now}{Guid.NewGuid().ToString()}");
                    Console.WriteLine("发布成功！");
                    Index -= 1;
                }
                Console.ReadKey();
            }

            #endregion

            #region 秒杀的code
            {
                try
                {
                    Console.WriteLine("秒杀开始。。。。。");
                    for (int i = 0; i < 200; i++)
                    {
                        Task.Run(() =>
                        {
                            MyRedisSubPublishHelper.LockByRedis("mstest");
                            string productCount = MyRedisHelper.StringGet("productcount");
                            int pcount = int.Parse(productCount);
                            if (pcount > 0)
                            {
                                long dlong = MyRedisHelper.StringDec("productcount");
                                Console.WriteLine($"秒杀成功，商品库存:{dlong}");
                                pcount -= 1;
                                System.Threading.Thread.Sleep(30);
                            }
                            else
                            {
                                Console.WriteLine($"秒杀失败，商品库存为零了！");
                                throw new Exception("产品秒杀数量为零！");//加载这里会比较保险
                                                    }
                            MyRedisSubPublishHelper.UnLockByRedis("mstest");
                        }).Wait();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"产品已经秒杀完毕，原因：{ex.Message}");
                }
                Console.ReadKey();
            }

            #endregion

        }
    }
}
