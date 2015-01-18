using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AllAboutManga.Webservice.MangaEden.Models
{
    [DataContract]
    public class PageCollection
    {
        [DataMember(Name = "images")]
        public IReadOnlyCollection<IReadOnlyCollection<object>> Pages { get; set; }
    }
}
