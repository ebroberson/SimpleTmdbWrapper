using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Serialization;

namespace SimpleTmdbWrapper.Models
{
    [DataContract]
    public sealed class Movie
    {
        [DataMember(Name = "adult")]
        public bool? Adult { get; set; }

        [DataMember(Name = "backdrop_path")]
        public string BackdropPath { get; set; }

        [DataMember(Name = "belongs_to_collection")]
        public BelongsToCollection BelongsToCollection { get; set; }

        [DataMember(Name = "budget")]
        public long Budget { get; set; }

        [DataMember(Name = "genres")]
        public IEnumerable<Genre> Genres { get; set; }

        [DataMember(Name = "homepage")]
        public string HomePage { get; set; }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "imdb_id")]
        public string ImdbId { get; set; }

        [DataMember(Name = "original_title")]
        public string OriginalTitle { get; set; }

        [DataMember(Name = "overview")]
        public string Overview { get; set; }

        [DataMember(Name = "popularity")]
        public double Popularity { get; set; }

        [DataMember(Name = "poster_path")]
        public string PosterPath { get; set; }

        [DataMember(Name = "production_companies")]
        public IEnumerable<Company> Companies { get; set; }

        [DataMember(Name = "production_countries")]
        public IEnumerable<Country> Countries { get; set; }

        [DataMember(Name = "release_date")]
        public DateTime? ReleaseDate { get; set; }

        [DataMember(Name = "revenue")]
        public long Revenue { get; set; }

        [DataMember(Name = "runtime")]
        public int Runtime { get; set; }

        [DataMember(Name = "spoken_languages")]
        public IEnumerable<Language> Languages { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "tagline")]
        public string Tagline { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "vote_average")]
        public double VoteAverage { get; set; }

        [DataMember(Name = "vote_count")]
        public int VoteCount { get; set; }

        [DataMember(Name = "alternative_titles/titles")]
        public IEnumerable<AlternativeTitles> AlternativeTitles { get; set; }

        [DataMember(Name = "credits")]
        public Credits Credits { get; set; }

        [DataMember(Name = "images")]
        public Images Images { get; set; }

        [DataMember(Name = "keywords")]
        public Keywords Keywords { get; set; }

        [DataMember(Name = "videos")]
        public Videos Videos { get; set; }

        [DataMember(Name = "translations")]
        public Translations Translations { get; set; }

        [DataMember(Name = "similar")]
        public Similar Similar { get; set; }

        [DataMember(Name = "reviews")]
        public Reviews Reviews { get; set; }

        [DataMember(Name = "lists")]
        public Lists Lists { get; set; }

        [DataMember(Name = "changes")]
        public Changes Changes { get; set; }
    }

    [DataContract]
    public class BelongsToCollection
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "poster_path")]
        public string PosterPath { get; set; }

        [DataMember(Name = "backdrop_path")]
        public string BackdropPath { get; set; }
    }

    [DataContract]
    public class AlternativeTitles
    {
        [DataMember(Name = "iso_3166_1")]
        public string Iso31661 { get; set; }

        [DataMember(Name = "title")]
        public string AltTitle { get; set; }
    }

    [DataContract]
    public class Credits
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "cast")]
        public IEnumerable<Cast> Cast { get; set; }

        [DataMember(Name = "crew")]
        public IEnumerable<Crew> Crew { get; set; }
    }

    [DataContract]
    public class Cast
    {
        [DataMember(Name = "cast_id")]
        public int CastId { get; set; }

        [DataMember(Name = "character")]
        public string Character { get; set; }

        [DataMember(Name = "credit_id")]
        public string CreditId { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "order")]
        public int Order { get; set; }

        [DataMember(Name = "profile_path")]
        public string ProfilePath { get; set; }
    }

    [DataContract]
    public class Crew
    {
        [DataMember(Name = "credit_id")]
        public string CreditId { get; set; }

        [DataMember(Name = "department")]
        public string Department { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "job")]
        public string Job { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "profile_path")]
        public string ProfilePath { get; set; }
    }

    [DataContract]
    public class Images
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "backdrops")]
        public IEnumerable<Image> Backdrops { get; set; }

        [DataMember(Name = "posters")]
        public IEnumerable<Image> Posters { get; set; }
    }

    [DataContract]
    public class Image
    {
        [DataMember(Name = "file_path")]
        public string FilePath { get; set; }

        [DataMember(Name = "width")]
        public int Width { get; set; }

        [DataMember(Name = "height")]
        public int Height { get; set; }

        [DataMember(Name = "iso_639_1")]
        public string Iso6391 { get; set; }

        [DataMember(Name = "aspect_ratio")]
        public double AspectRatio { get; set; }

        [DataMember(Name = "vote_average")]
        public double VoteAverage { get; set; }

        [DataMember(Name = "vote_count")]
        public int VoteCount { get; set; }

    }

    [DataContract]
    public class Keywords
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "keywords")]
        public IEnumerable<Keyword> RelatedKeywords { get; set; }
    }

    [DataContract]
    public class Keyword
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    [DataContract]
    public class Releases
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "countries")]
        public IEnumerable<Release> MovieReleases { get; set; }
    }

    [DataContract]
    public class Release
    {
        [DataMember(Name = "iso_3166_1")]
        public string Iso31661 { get; set; }

        [DataMember(Name = "certification")]
        public string Certification { get; set; }

        [DataMember(Name = "release_date")]
        public DateTime ReleaseDate { get; set; }
    }

    [DataContract]
    public class Videos
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "results")]
        public IEnumerable<Video> RelatedVideos { get; set; }
    }

    [DataContract]
    public class Video
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "iso_639_1")]
        public string Iso6391 { get; set; }

        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "site")]
        public string Site { get; set; }

        [DataMember(Name = "size")]
        public int Size { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract]
    public class Translations
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "translations")]
        public IEnumerable<Translation> MovieTranslations { get; set; }
    }

    [DataContract]
    public class Translation
    {
        [DataMember(Name = "iso_639_1")]
        public string Iso6391 { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "english_name")]
        public string EnglishName { get; set; }
    }

    [DataContract]
    public class Similar
    {
        public SearchResult<Movie> SimilarMovies { get; set; }
    }

    [DataContract]
    public class Reviews
    {
        public SearchResult<Review> MovieReviews { get; set; }
    }

    [DataContract]
    public class Review
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "author")]
        public string Author { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }

    [DataContract]
    public class Lists
    {
        public SearchResult<List> MovieLists { get; set; }
    }

    [DataContract]
    public class List
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "favorite_count")]
        public int FavoriteCount { get; set; }

        [DataMember(Name = "item_count")]
        public int ItemCount { get; set; }

        [DataMember(Name = "iso_639_1")]
        public string Iso6391 { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "poster_path")]
        public string PosterPath { get; set; }
    }

    [DataContract]
    public class Changes
    {
        [DataMember(Name = "changes")]
        public IEnumerable<Change> MovieChanges { get; set; }
    }

    [DataContract]
    public class Change
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "items")]
        public IEnumerable<ChangeItem> Items { get; set; }
    }

    [DataContract]
    public class ChangeItem
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "action")]
        public string Action { get; set; }

        [DataMember(Name = "time")]
        public DateTime Time { get; set; }

        [DataMember(Name = "value")]
        public ExpandoObject Value { get; set; }

        [DataMember(Name = "iso_639_1")]
        public string Iso6391 { get; set; }

        [DataMember(Name = "original_value")]
        public string OriginalValue { get; set; }


    }
}


