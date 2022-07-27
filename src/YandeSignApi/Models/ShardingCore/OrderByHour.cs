using System;

namespace YandeSignApi.Models.ShardingCore
{
    public class OrderByHour
    {
        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Name { get; set; }
    }
}
