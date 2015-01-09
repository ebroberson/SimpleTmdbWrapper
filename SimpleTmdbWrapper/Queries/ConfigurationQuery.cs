using Tmdb =SimpleTmdbWrapper.Models;

namespace SimpleTmdbWrapper.Queries
{
    public sealed class ConfigurationQuery : Query<Tmdb.Configuration>
    {
        public ConfigurationQuery(TmdbConfigProvider configProvider)
        {
            ApiMethod = "configuration";
            ConfigProvider = configProvider;
        }

        protected override string BuildRequestUrl()
        {
            var result = string.Format("{0}/{1}/{2}?{3}",
                                        ConfigProvider.ApiUrl,   // {0}
                                        ConfigProvider.ApiVersion, // {1}  
                                        ApiMethod,  // {2}
                                        ConfigProvider.ApiKey);    // {3}
            return result;
        }
    }
}
