using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SimpleTmdbWrapper.Models
{
    [DataContract]
    public class Configuration
    {
        [DataMember(Name = "images")]
        public ImageConfig Images { get; set; }

        [DataMember(Name = "change_keys")]
        public IEnumerable<string> ChangeKeys { get; set; }
    }

    [DataContract]
    public class ImageConfig
    {
        [DataMember(Name = "base_url")]
        public string BaseUrl { get; set; }

        [DataMember(Name = "secure_base_url")]
        public string SecureBaseUrl { get; set; }

        [DataMember(Name = "backdrop_sizes")]
        public IEnumerable<string> BackdropSizes { get; set; }

        [DataMember(Name = "logo_sizes")]
        public IEnumerable<string> LogoSizes { get; set; }

        [DataMember(Name = "poster_sizes")]
        public IEnumerable<string> PosterSizes { get; set; }

        [DataMember(Name = "profile_sizes")]
        public IEnumerable<string> ProfileSizes { get; set; }

        [DataMember(Name = "still_sizes")]
        public IEnumerable<string> StillSizes { get; set; }
    }
}
