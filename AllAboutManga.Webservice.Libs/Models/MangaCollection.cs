using System.Collections.Generic;

namespace AllAboutManga.Webservice.Libs.Models
{
    public class MangaCollection
    {
        public int End { get; set; }

        public IReadOnlyCollection<Manga> Mangas { get; set; }

        public int Page { get; set; }

        public int Start { get; set; }

        public int Total { get; set; }
    }
}
