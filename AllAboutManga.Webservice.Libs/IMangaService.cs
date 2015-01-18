using System.Threading.Tasks;
using AllAboutManga.Webservice.Libs.Models;

namespace AllAboutManga.Webservice.Libs
{
    public interface IMangaService
    {
        Task<Manga> GetByIdAsync(string mangaId);

        Task PopulateAsync(Manga manga);
    }
}
