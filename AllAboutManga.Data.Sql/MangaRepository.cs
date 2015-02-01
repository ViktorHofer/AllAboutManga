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
        private readonly SQLiteAsyncConnection _db;

        /// <summary>
        /// Hide constructor, only allow creation with asynchronous "constructor"
        /// </summary>
        private MangaRepository(SQLiteAsyncConnection db)
        {
            if (db == null) throw new ArgumentNullException(nameof(db));

            _db = db;
        }

        public static async Task<MangaRepository> CreateAsync(SQLiteAsyncConnection db)
        {
            var mangaRepository = new MangaRepository(db);

            await mangaRepository._db.CreateTablesAsync(
                typeof(Manga),
                typeof(Chapter),
                typeof(Page));

            return mangaRepository;
        }

        public async Task<bool> AnyAsync()
        {
            var numberOfMangas = await _db
                .Table<Manga>()
                .CountAsync();

            return numberOfMangas > 0;
        }

        public async Task CreateAsync(Manga manga)
        {
            if (manga == null) throw new ArgumentNullException(nameof(manga));

            await _db.InsertAsync(manga);
        }

        public async Task CreateAsync(IEnumerable<Manga> mangas)
        {
            if (mangas == null) throw new ArgumentNullException(nameof(mangas));

            await _db.InsertAllAsync(mangas);
        }

        public async Task ClearAsync()
        {
            await _db.DropTableAsync<Manga>();
            await _db.CreateTableAsync<Manga>();
        }

        public Task<IReadOnlyCollection<Manga>> QueryAsync(Func<Manga, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Manga> UpdateAsync(Manga manga)
        {
            if (manga == null) throw new ArgumentNullException(nameof(manga));

            await _db.UpdateAsync(manga);

            return manga;
        }

        public async Task DeleteAsync(string mangaId)
        {
            if (mangaId == null) throw new ArgumentNullException(nameof(mangaId));

            var deletedCount = await _db
                .DeleteAsync(new Manga() { Id = mangaId });
        }

        public async Task<Manga> GetByIdAsync(string mangaId)
        {
            if (mangaId == null) throw new ArgumentNullException(nameof(mangaId));

            return await _db
                .FindAsync<Manga>(mangaId);
        }

        public async Task<IReadOnlyCollection<Manga>> GetAllAsync()
        {
            return await _db
                .Table<Manga>()
                .ToListAsync();
        }
    }
}
