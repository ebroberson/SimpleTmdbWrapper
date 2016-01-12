using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTmdbWrapper.Limiters;

namespace SimpleTmdbWrapper
{
    /// <summary>
    /// Represents a way to get TMDB URL config and rate limiting settings
    /// </summary>
    public class TmdbConfigProvider
    {
        public string Version { get; private set; }

        public string Key { get; private set; }

        public string Url { get; private set; }

        public IRateLimiter RateLimiter { get; private set; }
        
        public TmdbConfigProvider(string url, string version, string key)
            : this(url, version, key, TimeSpanRateLimiter.Default)
        {
        }

        public TmdbConfigProvider(string url, string version, string key, IRateLimiter rateLimiter)
        {
            Url = url;
            Version = version;
            Key = key;
            RateLimiter = rateLimiter;
        }
    }
}
