using AllAboutManga.Data.Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AllAboutManga.Data.Libs
{
    public interface IMangaRepository
    {
        Task<bool> Any();

        Task Add(Manga manga);

        Task AddAll(IEnumerable<Manga> mangas);

        Task Clear();

        Task<IReadOnlyCollection<Manga>> Query(Func<Manga, bool> predicate);

        Task<Manga> Update(Manga manga);

        Task<bool> Delete(Manga manga);

        Task<Manga> GetById(string id);

        Task<IReadOnlyCollection<Manga>> GetAll();
    }
}
