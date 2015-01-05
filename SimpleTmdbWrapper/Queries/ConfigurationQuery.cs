using Tmdb =SimpleTmdbWrapper.Models;

namespace SimpleTmdbWrapper.Queries
{
    public sealed class ConfigurationQuery : Query<Tmdb.Configuration>
    {
        public ConfigurationQuery()
        {
            ApiMethod = "configuration";
        }

        protected override string BuildRequestUrl()
        {
            var result = string.Format("{0}/{1}/{2}?{3}",
                                        UrlStart,   // {0}
                                        ApiVersion, // {1}  
                                        ApiMethod,  // {2}
                                        ApiKey);    // {3}
            return result;
        }
    }
}
