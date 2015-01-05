using System.Runtime.Serialization;

namespace SimpleTmdbWrapper.Models
{
    [DataContract]
    public class Language
    {
        [DataMember(Name = "iso_639_1")]
        public string IsoCode { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
