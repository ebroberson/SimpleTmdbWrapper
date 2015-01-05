using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SimpleTmdbWrapper.Models
{
    [DataContract]
    public class Person
    {
        [DataMember(Name = "adult")]
        public bool? Adult { get; set; }

        [DataMember(Name = "also_known_as")]
        public IEnumerable<string> AlsoKnownAs { get; set; }

        [DataMember(Name = "biography")]
        public string Biography { get; set; }

        [DataMember(Name = "birthday")]
        public DateTime Birthday { get; set; }

        [DataMember(Name = "deathday")]
        public DateTime? Deathday { get; set; }

        [DataMember(Name = "homepage")]
        public string HomePage { get; set; }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name="place_of_birth")]
        public string PlaceOfBirth { get; set; }

        [DataMember(Name="profile_path")]
        public string ProfilePath { get; set; }
    }
}
