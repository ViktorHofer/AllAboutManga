using AllAboutManga.Business.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutManga.Business.Libs.Services
{
    public interface IMangaService
    {
        Task LoadAsync(bool loadOnlyIfNotExisting = true);

        Task<IReadOnlyCollection<Manga>> GetFavoritesAsync();

        Task<IReadOnlyCollection<Manga>> GetAllAsync();
    }
}
