using System;
using System.ComponentModel;
using Tmdb =SimpleTmdbWrapper.Models;

namespace SimpleTmdbWrapper.Queries
{
    public class PersonQuery : Query<Tmdb.Person>
    {
        public PersonQuery()
        {
            ApiMethod = "person";
        }

        public PersonQuery GetPerson(long id)
        {
            Arguments = string.Format("{0}/{1}", ApiMethod, id);
            return this;
        }
    }

    // the reason for this enum is to define what
    // methods can be appended to the response for 
    // a PersonQuery With call WITHOUT requiring
    // the user of this API to know the name of the
    // functions they want to use
    [Flags]
    public enum PersonAddons
    {
        [Description("credits")]
        Credits,
        [Description("images")]
        Images,
        [Description("changes")]
        Changes,
        [Description("popular")]
        Popular,
        [Description("latest")]
        Latest,
    }
}
