using System.Runtime.Serialization;

namespace SimpleTmdbWrapper.Models
{
    [DataContract]
    public class Genre
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
