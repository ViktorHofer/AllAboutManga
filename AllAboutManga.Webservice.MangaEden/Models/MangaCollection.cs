using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AllAboutManga.Webservice.MangaEden.Models
{
    [DataContract]
    public class MangaCollection
    {
        [DataMember(Name = "end")]
        public int End { get; set; }

        [DataMember(Name = "manga")]
        public IReadOnlyCollection<MangaMeta> Mangas { get; set; }

        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "start")]
        public int Start { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
