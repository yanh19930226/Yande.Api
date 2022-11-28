using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Configuration.RedisConfig
{
    /// Key Modes
    /// </summary>
    public enum RedisConfigrationKeyMode
    {
        /// <summary>
        /// key: /a/b/c
        /// prefixKey "/a/b/" => /a/b/c
        /// </summary>
        Default = 0,

        /// <summary>
        /// key: /a/b/c
        /// prefixKey "/a/b/" => /a/b/:c
        /// </summary>
        Json = 1,

        /// <summary>
        /// key: /a/b/c
        /// prefixKey "/a/b/" => c
        /// </summary>
        RemovePrefix = 2
    }
}
