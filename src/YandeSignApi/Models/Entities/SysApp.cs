using System;

namespace YandeSignApi.Models.Entities
{
    public class SysApp
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AppId { get; set; }

        public string Secret { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDel { get; set; }
    }
}
