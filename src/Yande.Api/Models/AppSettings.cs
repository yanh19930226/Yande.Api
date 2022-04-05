using System.Collections.Generic;

namespace Yande.Api.Models
{
    public class AppSettings
    {
        public string Str { get; set; }

        public int Num { get; set; }

        public List<int> Arr { get; set; }

        public SubObj SubObj { get; set; }
    }

    public class SubObj
    {
        public string a { get; set; }
    }
}
