using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllAboutManga.Data.Libs;
using AllAboutManga.Data.Libs.Models;
using SQLite;

namespace AllAboutManga.Data.Sql
{
    public class MangaRepository : IMangaRepository
    {
        private readonly SQLiteAsyncConnection _localDb;

        /// <summary>
        /// Hide constructor, only allow creation with asynchronous "constructor"
        /// </summary>
        private MangaRepository(SQLiteAsyncConnection localDb)
        {
            if (localDb == null) throw new ArgumentNullException(nameof(localDb));

            _localDb = localDb;
        }

        public static async Task<MangaRepository> CreateAsync(SQLiteAsyncConnection localDb)
        {
            var mangaRepository = new MangaRepository(localDb);

            await mangaRepository._localDb.CreateTablesAsync(
                typeof(Manga),
                typeof(Chapter),
                typeof(Page));

            return mangaRepository;
        }

        public async Task<bool> AnyAsync()
        {
            var numberOfMangas = await _localDb
                .Table<Manga>()
                .CountAsync();

            return numberOfMangas > 0;
        }

        public async Task CreateAsync(Manga manga)
        {
            if (manga == null) throw new ArgumentNullException(nameof(manga));

            await _localDb.InsertAsync(manga);
        }

        public async Task CreateAsync(IEnumerable<Manga> mangas)
        {
            if (mangas == null) throw new ArgumentNullException(nameof(mangas));

            await _localDb.InsertAllAsync(mangas);
        }

        public async Task ClearAsync()
        {
            await _localDb.DropTableAsync<Manga>();
            await _localDb.CreateTableAsync<Manga>();
        }

        public Task<IReadOnlyCollection<Manga>> QueryAsync(Func<Manga, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Manga> UpdateAsync(Manga manga)
        {
            if (manga == null) throw new ArgumentNullException(nameof(manga));

            await _localDb.UpdateAsync(manga);

            return manga;
        }

        public async Task DeleteAsync(string mangaId)
        {
            if (mangaId == null) throw new ArgumentNullException(nameof(mangaId));

            var deletedCount = await _localDb
                .DeleteAsync(new Manga() { Id = mangaId });
        }

        public async Task<Manga> GetByIdAsync(string mangaId)
        {
            if (mangaId == null) throw new ArgumentNullException(nameof(mangaId));

            return await _localDb
                .FindAsync<Manga>(mangaId);
        }

        public async Task<IReadOnlyCollection<Manga>> GetAllAsync()
        {
            return await _localDb
                .Table<Manga>()
                .ToListAsync();
        }
    }
}
