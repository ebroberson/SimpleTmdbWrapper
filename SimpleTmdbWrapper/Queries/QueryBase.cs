using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using NLog;
using Tmdb = SimpleTmdbWrapper.Models;

namespace SimpleTmdbWrapper.Queries
{
    public abstract class Query
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        protected TmdbConfigProvider ConfigProvider { get; set; }

        protected string ApiMethod { get; set; }

        protected virtual string Arguments { get; set; }

        protected virtual string SearchMethod => $"search/{ApiMethod}?query=";

        protected virtual string QueryAddons { get; set; }

        protected virtual bool HasAddons => !string.IsNullOrEmpty(QueryAddons);

        public virtual bool IsSearch { get; protected set; }

        public virtual string Method => IsSearch ? SearchMethod : ApiMethod + "/";

        public virtual IEnumerable<CultureInfo> SupportedLanguages { get; } = CultureInfo.GetCultures(CultureTypes.NeutralCultures);

        private string _language = "en";
        public virtual string Language
        {
            get
            {
                return _language;
            }

            set
            {
                if (IsSupportedLanguage(value))
                {
                    _language = value;
                }
                else
                {
                    throw new ArgumentException("Language is not supported", value);
                }
            }
        }

        public virtual bool IsSupportedLanguage(string iso6391)
        {
            return SupportedLanguages.Any(l => l.TwoLetterISOLanguageName == iso6391);
        }

        protected virtual TResult Execute<TResult>()
        {
            TResult result = default(TResult);

            var url = BuildRequestUrl();
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentLength = 0;

            _log.Debug($"Request created: {url}");

            try
            {
                ConfigProvider.RateLimiter.Limit(
                    new Task(() => {
                       using (var response = request.GetResponse())
                       {
                           using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                           {
                               result = JsonConvert.DeserializeObject<TResult>(reader.ReadToEnd());
                           }
                       }
                   })
                );

                return result;
            }
            finally
            {
                Reset();
            }
        }

        protected virtual string BuildRequestUrl()
        {
            if (string.IsNullOrEmpty(ApiMethod))
            {
                throw new ArgumentNullException(nameof(ApiMethod));
            }

            var arguments = HttpUtility.UrlEncode(Arguments);
            var addons = HasAddons ? QueryAddons + "&" : IsSearch ? "&" : "?";
            var result = $"{ConfigProvider.Url}/{ConfigProvider.Version}/{Method}{arguments}{addons}{ConfigProvider.Key}";
            return result;
        }

        protected virtual void AppendAddons(string query)
        {
            if (!HasAddons)
            {
                QueryAddons += "?append_to_response=" + query;
            }
        }

        public virtual void Reset()
        {
            IsSearch = false;
            Arguments = string.Empty;
            QueryAddons = string.Empty;
        }
    }

    public abstract class Query<T> : Query
    {
        /// <summary>
        /// Performs a Search. Mutually exclusive to <see cref="With" />
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <remarks>Works with Company, Collection, Keyword, List, Movie, Person, TV </remarks>
        public virtual Tmdb.SearchResult<T> Search(string query)
        {
            IsSearch = true;
            Arguments = query;
            QueryAddons = string.Empty;

            var result = Execute<Tmdb.SearchResult<T>>();
            return result;
        }

        public virtual T Execute()
        {
            IsSearch = false;
            return Execute<T>();
        }

        public virtual Query<T> With(Enum addons)
        {
            var descriptions = addons.GetActiveFlagDescriptions();
            var toAppend = string.Join(",", descriptions);
            AppendAddons(toAppend);

            return this;
        }
    }
}
