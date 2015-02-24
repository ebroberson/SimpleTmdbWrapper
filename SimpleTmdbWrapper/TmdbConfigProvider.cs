using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTmdbWrapper
{
    /// <summary>
    /// Represents a way to get TMDB URL config and rate limiting settings
    /// </summary>
    public abstract class TmdbConfigProvider
    {
        public abstract string ApiVersion
        {
            get;
        }

        public abstract string ApiKey
        {
            get;
        }

        public abstract string ApiUrl
        {
            get;
        }

        public abstract Limiters.IRateLimiter RateLimiter
        {
            get;
        }
    }
}
