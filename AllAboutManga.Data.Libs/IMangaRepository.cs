using AllAboutManga.Data.Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AllAboutManga.Data.Libs
{
    public interface IMangaRepository
    {
        Task<bool> AnyAsync();

        Task CreateAsync(Manga manga);

        Task CreateAsync(IEnumerable<Manga> mangas);

        Task ClearAsync();

        Task<IReadOnlyCollection<Manga>> QueryAsync(Func<Manga, bool> predicate);

        Task<Manga> UpdateAsync(Manga manga);

        Task DeleteAsync(string mangaId);

        Task<Manga> GetByIdAsync(string mangaId);

        Task<IReadOnlyCollection<Manga>> GetAllAsync();
    }
}
