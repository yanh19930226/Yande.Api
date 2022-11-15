using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In66.Authentication.Rsa
{
    public class RsaEvents
    {
        public Func<RsaValidatedContext, Task> OnTokenValidated { get; set; }
    }
}
