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
            var result = $"{ConfigProvider.Url}/{ConfigProvider.Version}/{ApiMethod}{ConfigProvider.Key}";
            return result;
        }
    }
}
