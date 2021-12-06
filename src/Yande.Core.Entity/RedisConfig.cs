using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yande.Core.Entity
{
    public class RedisConfig
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Timeout { get; set; }
        public int Db { get; set; }
    }
}
