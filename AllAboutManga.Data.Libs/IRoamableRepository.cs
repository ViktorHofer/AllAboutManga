using AllAboutManga.Data.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutManga.Data.Libs
{
    public interface IRoamableRepository
    {
        Task<IReadOnlyCollection<Manga>> GetFavorites();

        Task<IReadOnlyCollection<Manga>> GetHistory();
    }
}
