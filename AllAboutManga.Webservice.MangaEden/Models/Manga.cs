using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AllAboutManga.Webservice.MangaEden.Models
{
    [DataContract]
    public class Manga
    {
        [DataMember(Name = "aka")]
        public IReadOnlyCollection<object> Aka { get; set; }

        [DataMember(Name = "aka-alias")]
        public IReadOnlyCollection<object> AkaAlias { get; set; }

        [DataMember(Name = "alias")]
        public string Alias { get; set; }

        [DataMember(Name = "artist")]
        public string Artist { get; set; }

        [DataMember(Name = "artist_kw")]
        public IReadOnlyCollection<object> ArtistKw { get; set; }

        [DataMember(Name = "author")]
        public string Author { get; set; }

        [DataMember(Name = "author_kw")]
        public IReadOnlyCollection<object> AuthorKw { get; set; }

        [DataMember(Name = "categories")]
        public IReadOnlyCollection<string> Categories { get; set; }

        [DataMember(Name = "chapters")]
        public IReadOnlyCollection<IReadOnlyCollection<object>> Chapters { get; set; }

        [DataMember(Name = "chapters_len")]
        public int ChaptersLength { get; set; }

        [DataMember(Name = "created")]
        public double Created { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "hits")]
        public int Hits { get; set; }

        [DataMember(Name = "image")]
        public string Image { get; set; }

        [DataMember(Name = "language")]
        public int Language { get; set; }

        [DataMember(Name = "last_chapter_date")]
        public double LastChapterDate { get; set; }

        [DataMember(Name = "random")]
        public IReadOnlyCollection<double> Random { get; set; }

        [DataMember(Name = "released")]
        public object Released { get; set; }

        [DataMember(Name = "startsWith")]
        public string StartsWith { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "title_kw")]
        public IReadOnlyCollection<string> TitleKw { get; set; }

        [DataMember(Name = "updatedKeywords")]
        public bool UpdatedKeywords { get; set; }
    }
}
