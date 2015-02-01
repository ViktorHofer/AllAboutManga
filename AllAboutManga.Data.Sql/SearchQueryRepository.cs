using AllAboutManga.Data.Libs;
using AllAboutManga.Data.Libs.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutManga.Data.Sql
{
    public class SearchQueryRepository : ISearchQueryRepository
    {
        private readonly SQLiteAsyncConnection _db;

        /// <summary>
        /// Hide constructor, only allow creation with asynchronous "constructor"
        /// </summary>
        private SearchQueryRepository(SQLiteAsyncConnection db)
        {
            if (db == null) throw new ArgumentNullException(nameof(db));

            _db = db;
        }

        public static async Task<SearchQueryRepository> CreateAsync(SQLiteAsyncConnection db)
        {
            var searchItemRepository = new SearchQueryRepository(db);

            await searchItemRepository._db.CreateTablesAsync(typeof(SearchQuery));

            return searchItemRepository;
        }


        public async Task<IReadOnlyCollection<SearchQuery>> GetSearchHistoryAsync()
        {
            return await _db
                .Table<SearchQuery>()
                .ToListAsync();
        }
    }
}
