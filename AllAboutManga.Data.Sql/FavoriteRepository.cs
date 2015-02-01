using AllAboutManga.Data.Libs;
using AllAboutManga.Data.Libs.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutManga.Data.Sql
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly SQLiteAsyncConnection _db;

        /// <summary>
        /// Hide constructor, only allow creation with asynchronous "constructor"
        /// </summary>
        private FavoriteRepository(SQLiteAsyncConnection db)
        {
            if (db == null) throw new ArgumentNullException(nameof(db));

            _db = db;
        }

        public static async Task<FavoriteRepository> CreateAsync(SQLiteAsyncConnection db)
        {
            var favoriteRepository = new FavoriteRepository(db);

            await favoriteRepository._db.CreateTablesAsync(typeof(Favorite));

            return favoriteRepository;
        }

        public async Task<IReadOnlyCollection<Favorite>> GetFavoritesAsync()
        {
            return await _db
                .Table<Favorite>()
                .ToListAsync();
        }
    }
}
