using SQLite;

namespace AllAboutManga.Data.Libs.Models
{
    [Table(nameof(Favorite) + "s")]
    public class Favorite
    {
        [PrimaryKey]
        public string MangaId { get; set; }
    }
}
