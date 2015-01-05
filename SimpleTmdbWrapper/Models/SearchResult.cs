using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SimpleTmdbWrapper.Models
{
    [DataContract]
    public class SearchResult
    {
        /// <summary>
        /// Does not always get filled.  Used on Reviews, Lists, et al.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "total_pages")]
        public int TotalPages { get; set; }

        [DataMember(Name = "total_results")]
        public int TotalResults { get; set; }
    }

    [DataContract]
    public class SearchResult<T> : SearchResult
    {
        [DataMember(Name = "results")]
        public IEnumerable<T> Results { get; set; }
    }
}
