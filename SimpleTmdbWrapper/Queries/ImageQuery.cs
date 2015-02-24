using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tmdb =SimpleTmdbWrapper.Models;

namespace SimpleTmdbWrapper.Queries
{
    public class ImageQuery : Query
    {
        public Tmdb.ImageConfig ImageConfig { get; set; }
        public Tmdb.Movie Movie { get; set; }
        public ImageType ImageType { get; set; }

        public static readonly string DefaultSize = "original";
        public static readonly ImageType DefaultImageType = ImageType.Poster;

        public ImageQuery(TmdbConfigProvider configProvider)
        {
        }

        public ImageQuery(TmdbConfigProvider configProvider, Tmdb.ImageConfig imageConfig, Tmdb.Movie movie)
            : this(configProvider, imageConfig, movie, DefaultImageType)
        {
        }

        public ImageQuery(TmdbConfigProvider configProvider, Tmdb.Configuration config, Tmdb.Movie movie)
            : this(configProvider, config.Images, movie, DefaultImageType)
        {
        }

        public ImageQuery(TmdbConfigProvider configProvider, Tmdb.Configuration config, Tmdb.Movie movie, ImageType imageType)
            : this(configProvider, config.Images, movie, imageType)
        {
        }

        public ImageQuery(TmdbConfigProvider configProvider, Tmdb.ImageConfig imageConfig, Tmdb.Movie movie, ImageType imageType)
        {
            ConfigProvider = configProvider;
            ImageConfig = imageConfig;
            Movie = movie;
            ImageType = imageType;
        }

        protected override string BuildRequestUrl()
        {
            return BuildRequestUrl(false, DefaultSize, ImageType);
        }

        protected string BuildRequestUrl(bool secure)
        {
            return BuildRequestUrl(secure, DefaultSize, ImageType);
        }

        protected string BuildRequestUrl(bool secure, string size)
        {
            return BuildRequestUrl(secure, size, ImageType);
        }

        protected string BuildRequestUrl(bool secure, string size, ImageType type)
        {
            var result = string.Format("{0}{1}{2}",
                                        secure ? ImageConfig.SecureBaseUrl : ImageConfig.BaseUrl,
                                        size,
                                        GetPath(type)
                                      );

            return result;
        }

        /// <summary>
        /// This returns a Stream, so make sure to Dispose of it!
        /// </summary>
        /// <returns></returns>
        public async Task<Stream> ExecuteAsync()
        {
            Stream result = null;
            var url = BuildRequestUrl();
            var request = WebRequest.Create(url) as HttpWebRequest;
            var response = await request.GetResponseAsync();

            result = response.GetResponseStream();
            Reset();

            return result;
        }

        private string GetPath(ImageType imageType)
        {
            var result = string.Empty;

            switch (imageType)
            {
                case ImageType.Backdrop:
                    result = Movie.BackdropPath;
                    break;
                case ImageType.Poster:
                    result = Movie.PosterPath;
                    break;
                case ImageType.Logo:
                    result = string.Empty; // TV?
                    break;
                case ImageType.Still:
                    result = string.Empty; // TV?
                    break;
                default:
                    result = string.Empty;
                    break;
            }

            return result;
        }
    }

    public enum ImageType
    {
        Backdrop,
        Poster,
        Logo,
        Still
    }
}
