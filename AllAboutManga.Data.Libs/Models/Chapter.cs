using SQLite;

namespace AllAboutManga.Data.Libs.Models
{
    [Table("Chapters")]
    public class Chapter
    {
        [PrimaryKey]
        public string Id { get; set; }

        [NotNull]
        public string MangaId { get; set; }

        public long Number { get; set; }

        public double Date { get; set; }

        public string Title { get; set; }
    }
}
