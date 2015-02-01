using SQLite;

namespace AllAboutManga.Data.Libs.Models
{
    [Table("Pages")]
    public class Page
    {
        [NotNull]
        public string ChapterId { get; set; }

        public long Number { get; set; }

        public string Image { get; set; }

        public long Height { get; set; }

        public long Width { get; set; }
    }
}
