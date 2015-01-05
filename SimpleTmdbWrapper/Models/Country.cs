using System.Runtime.Serialization;

namespace SimpleTmdbWrapper.Models
{
    [DataContract]
    public class Country
    {
        [DataMember(Name = "iso_3166_1")]
        public string IsoCode { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
