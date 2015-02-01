using SQLite;
using System;

namespace AllAboutManga.Data.Libs.Models
{
    [Table("SearchHistory")]
    public class SearchQuery
    {
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public DateTime SearchDate { get; set; }
    }
}
