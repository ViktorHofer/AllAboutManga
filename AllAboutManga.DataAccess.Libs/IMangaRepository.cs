using AllAboutManga.DataAccess.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutManga.DataAccess.Libs
{
    public interface IMangaRepository
    {
        Task<bool> Any();

        Task Add(Manga manga);

        Task AddAll(IEnumerable<Manga> mangas);

        Task Clear();

        Task<Manga> Update(Manga manga);

        Task<bool> Delete(Manga manga);

        Task<Manga> GetById(string id);

        Task<IReadOnlyCollection<Manga>> GetAll();
    }
}
