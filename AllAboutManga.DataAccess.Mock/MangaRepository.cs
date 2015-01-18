using AllAboutManga.DataAccess.Libs;
using System;
using System.Collections.Generic;
using AllAboutManga.DataAccess.Libs.Models;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;

namespace AllAboutManga.DataAccess.Mock
{
    public class MangaRepository : IMangaRepository
    {
        private readonly List<Manga> _mangaDb = new List<Manga>();

        public async Task<bool> Any()
        {
            await Task.FromResult<object>(null);
            return _mangaDb.Count > 0;
        }

        public async Task Add(Manga manga)
        {
            _mangaDb.Add(manga);
            await Task.FromResult<object>(null);
        }

        public async Task AddAll(IEnumerable<Manga> mangas)
        {
            _mangaDb.AddRange(mangas);
            await Task.FromResult<object>(null);
        }

        public async Task Clear()
        {
            _mangaDb.Clear();
            await Task.FromResult<object>(null);
        }

        public async Task<bool> Delete(Manga manga)
        {
            await Task.FromResult<object>(null);
            return _mangaDb.Remove(manga);
        }

        public async Task<Manga> GetById(string id)
        {
            await Task.FromResult<object>(null);
            return _mangaDb.Find(manga => manga.Id == id);
        }

        public async Task<Manga> Update(Manga newManga)
        {
            var oldManga = _mangaDb.Find(manga => manga.Id == newManga.Id);
            if (oldManga != null)
            {
                _mangaDb.Remove(oldManga);
            }

            _mangaDb.Add(newManga);
            await Task.FromResult<object>(null);
            return newManga;
        }

        public async Task<IReadOnlyCollection<Manga>> GetAll()
        {
            await Task.FromResult<object>(null);
            return _mangaDb;
        }

        public async Task<IReadOnlyCollection<Manga>> Query(Func<Manga, bool> predicate)
        {
            await Task.FromResult<object>(null);

            return _mangaDb
                .Where(predicate)
                .ToList();
        }
    }
}
