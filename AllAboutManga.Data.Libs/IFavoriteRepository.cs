using AllAboutManga.Data.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutManga.Data.Libs
{
    public interface IFavoriteRepository
    {
        Task<IReadOnlyCollection<Favorite>> GetFavoritesAsync();
    }
}
