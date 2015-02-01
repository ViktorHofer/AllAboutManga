using SQLite;
using System;
using System.Collections.Generic;

namespace AllAboutManga.Data.Libs.Models
{
    [Table("Mangas")]
    public class Manga
    {
        [PrimaryKey]
        public string Id { get; set; }

        [NotNull]
        public string Title { get; set; }

        public string Categories { get; set; }

        public string Alias { get; set; }

        public string Artist { get; set; }

        public string Author { get; set; }

        public DateTime Created { get; set; }

        public string Description { get; set; }

        public int Hits { get; set; }

        public string Image { get; set; }

        public int Language { get; set; }

        public DateTime LastChapterDate { get; set; }

        public int Status { get; set; }
    }
}
