using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In66.Authentication.Rsa
{
    public class RsaValidatedContext : ResultContext<RsaSchemeOptions>
    {
        public RsaValidatedContext(HttpContext context, AuthenticationScheme scheme, RsaSchemeOptions options)
            : base(context, scheme, options)
        {
        }
    }
}
