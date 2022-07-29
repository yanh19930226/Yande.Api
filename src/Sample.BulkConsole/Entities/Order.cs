using ShardingCore.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.BulkConsole.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public string OrderNo { get; set; }
        public int Seq { get; set; }
        [ShardingTableKey]
        public DateTime CreateTime { get; set; }
    }
}
