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

        protected TmdbConfigProvider ConfigProvider
        {
            get;
            set;
        }

        protected string ApiMethod
        {
            get;
            set;
        }

        protected virtual string Arguments
        {
            get;
            set;
        }

        protected virtual string SearchMethod
        {
            get
            {
                return string.Format("{0}/{1}?query=", "search", ApiMethod);
            }
        }

        protected virtual string QueryAddons
        {
            get;
            set;
        }

        protected virtual bool HasAddons
        {
            get
            {
                return !string.IsNullOrEmpty(QueryAddons);
            }
        }

        public virtual bool IsSearch
        {
            get;
            protected set;
        }

        public virtual string Method
        {
            get
            {
                return IsSearch ? SearchMethod : ApiMethod + "/";
            }
        }

        private IEnumerable<CultureInfo> supportedLanguages = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
        public virtual IEnumerable<CultureInfo> SupportedLanguages
        {
            get
            {
                return supportedLanguages;
            }
        }

        private string language = "en";
        public virtual string Language
        {
            get
            {
                return language;
            }

            set
            {
                if (IsSupportedLanguage(value))
                {
                    language = value;
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

            _log.Debug(string.Format("Request created: {0}", url));

            using (var response = request.GetResponse())
            {
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    result = JsonConvert.DeserializeObject<TResult>(reader.ReadToEnd());
                }
            }

            Reset();
            return result;
        }

        protected virtual async Task<TResult> ExecuteAsync<TResult>()
        {
            TResult result = default(TResult);

            var url = BuildRequestUrl();
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentLength = 0;

            _log.Debug(string.Format("Request created: {0}", url));

            await Task.Run(() =>
                ConfigProvider.RateLimiter.Limit(Task.Run(async () =>
                    {
                        using (var response = await request.GetResponseAsync())
                        {
                            using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                            {
                                result = await Task.Run<TResult>(() => JsonConvert.DeserializeObject<TResult>(reader.ReadToEnd()));
                            }
                        }
                    })
                ));

            Reset();
            return result;
        }

        protected virtual string BuildRequestUrl()
        {
            if (string.IsNullOrEmpty(ApiMethod))
            {
                throw new ArgumentException("ApiMethod is null or empty");
            }

            var result = string.Format("{0}/{1}/{2}{3}{4}{5}",
                                        ConfigProvider.ApiUrl,   // {0}
                                        ConfigProvider.ApiVersion, // {1}  
                                        Method,  // {2}
                                        HttpUtility.UrlEncode(Arguments),  // {3}
                                        HasAddons ? QueryAddons + "&" : IsSearch ? "&" : "?", // {4}
                                        ConfigProvider.ApiKey);    // {5}
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
        public virtual Tmdb.SearchResult<T> Search(string query)
        {
            IsSearch = true;
            Arguments = query;
            QueryAddons = string.Empty;

            var result = Execute<Tmdb.SearchResult<T>>();
            return result;
        }

        /// <summary>
        /// Performs a Search. Mutually exclusive to <see cref="With" />
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <remarks>Works with Company, Collection, Keyword, List, Movie, Person, TV </remarks>
        public virtual async Task<Tmdb.SearchResult<T>> SearchAsync(string query)
        {
            IsSearch = true;
            Arguments = query;
            QueryAddons = string.Empty;

            var result = await ExecuteAsync<Tmdb.SearchResult<T>>();
            return result;
        }

        public virtual T Execute()
        {
            IsSearch = false;
            return Execute<T>();
        }

        public virtual async Task<T> ExecuteAsync()
        {
            IsSearch = false;
            return await ExecuteAsync<T>();
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
