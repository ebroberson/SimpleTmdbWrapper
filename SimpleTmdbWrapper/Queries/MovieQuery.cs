
using System;
using System.ComponentModel;
using Tmdb = SimpleTmdbWrapper.Models;

namespace SimpleTmdbWrapper.Queries
{
    public class MovieQuery : Query<Tmdb.Movie>
    {
        public MovieAddons Addons { get; private set; }

        public MovieQuery(TmdbConfigProvider configProvider)
        {
            ApiMethod = "movie";
            Addons = MovieAddons.None;
            ConfigProvider = configProvider;
        }
        
        /// <summary>
        /// Retrieves info for a TMDB movie.  Includes Credits and Videos MovieAddons.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MovieQuery GetMovie(long id)
        {
            IsSearch = false;
            Arguments = id.ToString();
            return (MovieQuery)this.With(MovieAddons.Credits | MovieAddons.Videos);
        }

        public MovieQuery GetMovie(string id, MovieIdType type)
        {
            MovieQuery result = this;

            switch (type)
            {
                case MovieIdType.NONE:
                    throw new ArgumentException("Invalid MovieIdType specified.", nameof(type));
                case MovieIdType.IMDB:
                    throw new NotImplementedException();
                case MovieIdType.TMDB:
                    result = GetMovie(long.Parse(id));
                    break;
            }

            return result;
        }

        public MovieQuery ResetAddons()
        {
            IsSearch = false;
            Addons = MovieAddons.None;
            return this;
        }
    }

    public enum MovieIdType
    {
        NONE,
        IMDB,
        TMDB
    }

    public enum MovieSearchType
    {
        [Description("phrase")]
        Regular,
        [Description("ngram")]
        Autocomplete
    }

    // the reason for this enum is to define what
    // methods can be appended to the response for 
    // a MovieQuery With call WITHOUT requiring
    // the user of this API to know the name of the
    // functions they want to use
    [Flags]
    public enum MovieAddons
    {
        [Description("none")]
        None = 0,
        [Description("alternative_titles")]
        AlternativeTitles = 1,
        [Description("changes")]
        Changes = 2,
        [Description("images")]
        Images = 4,
        [Description("credits")]
        Credits = 8,
        [Description("keywords")]
        Keywords = 16,
        [Description("releases")]
        Releases = 32,
        [Description("videos")]
        Videos = 64,
        [Description("translations")]
        Translations = 128,
        [Description("similar")]
        SimliarMovies = 256,
        [Description("reviews")]
        Reviews = 512,
        [Description("lists")]
        Lists = 1024//,
        //[Description("latest")]
        //Latest = 2048,
        //[Description("upcoming")]
        //Upcoming = 4096,
        //[Description("now_playing")]
        //NowPlaying = 8192,
        //[Description("popular")]
        //Popular = 16384,
        //[Description("top_rated")]
        //TopRated = 32768,
        //[Description("account_states")]
        //AccountStates = 65536,
        //[Description("rating")]
        //Rating = 131072
    }

}
