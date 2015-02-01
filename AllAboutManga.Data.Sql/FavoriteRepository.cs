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
        private readonly SQLiteAsyncConnection _roamingDb;

        /// <summary>
        /// Hide constructor, only allow creation with asynchronous "constructor"
        /// </summary>
        private FavoriteRepository(SQLiteAsyncConnection roamingDb)
        {
            if (roamingDb == null) throw new ArgumentNullException(nameof(roamingDb));

            _roamingDb = roamingDb;
        }

        public static async Task<FavoriteRepository> CreateAsync(SQLiteAsyncConnection roamingDb)
        {
            var favoriteRepository = new FavoriteRepository(roamingDb);

            await favoriteRepository._roamingDb.CreateTablesAsync(typeof(Favorite));

            return favoriteRepository;
        }

        public async Task<IReadOnlyCollection<Favorite>> GetFavoritesAsync()
        {
            return await _roamingDb
                .Table<Favorite>()
                .ToListAsync();
        }
    }
}
