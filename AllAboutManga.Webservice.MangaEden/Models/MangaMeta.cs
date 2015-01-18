using System.Runtime.Serialization;

namespace AllAboutManga.Webservice.MangaEden.Models
{
    [DataContract]
    public class MangaMeta
    {
        [DataMember(Name = "a")]
        public string Alias { get; set; }

        [DataMember(Name = "h")]
        public int Hits { get; set; }

        [DataMember(Name = "i")]
        public string Id { get; set; }

        [DataMember(Name = "im")]
        public string Image { get; set; }

        [DataMember(Name = "ld")]
        public double LastChapterDate { get; set; }

        [DataMember(Name = "s")]
        public int Status { get; set; }

        [DataMember(Name = "t")]
        public string Title { get; set; }
    }
}
