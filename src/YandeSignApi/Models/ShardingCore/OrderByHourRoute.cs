﻿using MySqlConnector;
using ShardingCore.Core.EntityMetadatas;
using ShardingCore.Core.PhysicTables;
using ShardingCore.Core.VirtualDatabase.VirtualDataSources.Abstractions;
using ShardingCore.Core.VirtualDatabase.VirtualTables;
using ShardingCore.Core.VirtualRoutes;
using ShardingCore.Core.VirtualRoutes.TableRoutes.Abstractions;
using ShardingCore.Exceptions;
using ShardingCore.Extensions;
using ShardingCore.TableCreator;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using YandeSignApi.Data;

namespace YandeSignApi.Models.ShardingCore
{
    /// <summary>
    /// OrderByHourRoute
    /// </summary>
    public class OrderByHourRoute : AbstractShardingOperatorVirtualTableRoute<OrderByHour, DateTime>
    {
        private const string Tables = "Tables";
        private const string TABLE_SCHEMA = "TABLE_SCHEMA";
        private const string TABLE_NAME = "TABLE_NAME";
        private const string CurrentTableName = nameof(OrderByHour);
        private readonly IVirtualDataSourceManager<DefaultDbContext> _virtualDataSourceManager;
        private readonly IVirtualTableManager<DefaultDbContext> _virtualTableManager;
        private readonly IShardingTableCreator<DefaultDbContext> _shardingTableCreator;
        private readonly ConcurrentDictionary<string, object?> _tails = new ConcurrentDictionary<string, object?>();
        private readonly object _lock = new object();
        /// <summary>
        /// OrderByHourRoute
        /// </summary>
        /// <param name="virtualDataSourceManager"></param>
        /// <param name="virtualTableManager"></param>
        /// <param name="shardingTableCreator"></param>
        public OrderByHourRoute(IVirtualDataSourceManager<DefaultDbContext> virtualDataSourceManager, IVirtualTableManager<DefaultDbContext> virtualTableManager, IShardingTableCreator<DefaultDbContext> shardingTableCreator)
        {
            _virtualDataSourceManager = virtualDataSourceManager;
            _virtualTableManager = virtualTableManager;
            _shardingTableCreator = shardingTableCreator;
        }

        /// <summary>
        /// ShardingKeyToTail
        /// </summary>
        /// <param name="shardingKey"></param>
        /// <returns></returns>
        public override string ShardingKeyToTail(object shardingKey)
        {
            var dateTime = (DateTime)shardingKey;
            return ShardingKeyFormat(dateTime);
        }
        /// <summary>
        /// ShardingKeyFormat
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private string ShardingKeyFormat(DateTime dateTime)
        {
            var tail = $"{dateTime:yyyyMMddHH}";

            return tail;
        }
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityMetadataTableBuilder<OrderByHour> builder)
        {
            builder.ShardingProperty(o => o.CreateTime);
        }
        /// <summary>
        /// 如果你是非mysql数据库请自行实现这个方法返回当前类在数据库已经存在的后缀
        /// 仅启动时调用
        /// </summary>
        /// <returns></returns>
        public override List<string> GetAllTails()
        {
            //启动寻找有哪些表后缀
            using (var connection = new MySqlConnection(_virtualDataSourceManager.GetCurrentVirtualDataSource().DefaultConnectionString))
            {
                connection.Open();
                var database = connection.Database;

                using (var dataTable = connection.GetSchema(Tables))
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        var schema = dataTable.Rows[i][TABLE_SCHEMA];
                        if (database.Equals($"{schema}", StringComparison.OrdinalIgnoreCase))
                        {
                            var tableName = dataTable.Rows[i][TABLE_NAME]?.ToString() ?? string.Empty;
                            if (tableName.StartsWith(CurrentTableName, StringComparison.OrdinalIgnoreCase))
                            {
                                //如果没有下划线那么需要CurrentTableName.Length有下划线就要CurrentTableName.Length+1
                                _tails.TryAdd(tableName.Substring(CurrentTableName.Length), null);
                            }
                        }
                    }
                }
            }
            return _tails.Keys.ToList();
        }
        /// <summary>
        /// RouteWithValue
        /// </summary>
        /// <param name="allPhysicTables"></param>
        /// <param name="shardingKey"></param>
        /// <returns></returns>
        /// <exception cref="ShardingCoreException"></exception>
        public override IPhysicTable RouteWithValue(List<IPhysicTable> allPhysicTables, object shardingKey)
        {
            var shardingKeyToTail = ShardingKeyToTail(shardingKey);

            if (!_tails.TryGetValue(shardingKeyToTail, out var _))
            {
                lock (_lock)
                {
                    if (!_tails.TryGetValue(shardingKeyToTail, out var _))
                    {
                        var virtualTable = _virtualTableManager.GetVirtualTable(typeof(OrderByHour));


                        //必须先执行AddPhysicTable在进行CreateTable
                        _virtualTableManager.AddPhysicTable(virtualTable, new DefaultPhysicTable(virtualTable, shardingKeyToTail));
                        try
                        {
                            _shardingTableCreator.CreateTable<OrderByHour>(_virtualDataSourceManager.GetCurrentVirtualDataSource().DefaultDataSourceName, shardingKeyToTail);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("尝试添加表失败" + ex);
                        }

                        _tails.TryAdd(shardingKeyToTail, null);
                    }
                }
            }

            var needRefresh = allPhysicTables.Count != _tails.Count;
            if (needRefresh)
            {
                var virtualTable = _virtualTableManager.GetVirtualTable(typeof(OrderByHour));
                //修复可能导致迭代器遍历时添加的bug
                var keys = _tails.Keys.ToList();
                foreach (var tail in keys)
                {
                    var hashSet = allPhysicTables.Select(o => o.Tail).ToHashSet();
                    if (!hashSet.Contains(tail))
                    {
                        var tables = virtualTable.GetAllPhysicTables();
                        var physicTable = tables.FirstOrDefault(o => o.Tail == tail);
                        if (physicTable != null)
                        {
                            allPhysicTables.Add(physicTable);
                        }
                    }
                }
            }
            var physicTables = allPhysicTables.Where(o => o.Tail == shardingKeyToTail).ToList();
            if (physicTables.IsEmpty())
            {
                throw new ShardingCoreException($"sharding key route not match {EntityMetadata.EntityType} -> [{EntityMetadata.ShardingTableProperty.Name}] ->【{shardingKey}】 all tails ->[{string.Join(",", allPhysicTables.Select(o => o.FullName))}]");
            }

            if (physicTables.Count > 1)
                throw new ShardingCoreException($"more than one route match table:{string.Join(",", physicTables.Select(o => $"[{o.FullName}]"))}");
            return physicTables[0];
        }

        /// <summary>
        /// GetRouteToFilter
        /// </summary>
        /// <param name="shardingKey"></param>
        /// <param name="shardingOperator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Expression<Func<string, bool>> GetRouteToFilter(DateTime shardingKey, ShardingOperatorEnum shardingOperator)
        {
            var t = ShardingKeyFormat(shardingKey);
            switch (shardingOperator)
            {
                case ShardingOperatorEnum.GreaterThan:
                case ShardingOperatorEnum.GreaterThanOrEqual:
                    return tail => String.Compare(tail, t, StringComparison.Ordinal) >= 0;
                case ShardingOperatorEnum.LessThan:
                    {
                        var currentHourBeginTime = new DateTime(shardingKey.Year, shardingKey.Month, shardingKey.Day, shardingKey.Hour, 0, 0);
                        //处于临界值 o=>o.time < [2021-01-01 00:00:00] 尾巴20210101不应该被返回
                        if (currentHourBeginTime == shardingKey)
                            return tail => String.Compare(tail, t, StringComparison.Ordinal) < 0;
                        return tail => String.Compare(tail, t, StringComparison.Ordinal) <= 0;
                    }
                case ShardingOperatorEnum.LessThanOrEqual:
                    return tail => String.Compare(tail, t, StringComparison.Ordinal) <= 0;
                case ShardingOperatorEnum.Equal: return tail => tail == t;
                default:
                    {
#if DEBUG
                    Console.WriteLine($"shardingOperator is not equal scan all table tail");
#endif
                        return tail => true;
                    }
            }
        }
    }
}
