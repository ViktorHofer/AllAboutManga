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
        private readonly SQLiteAsyncConnection _roamingDb;

        /// <summary>
        /// Hide constructor, only allow creation with asynchronous "constructor"
        /// </summary>
        private SearchQueryRepository(SQLiteAsyncConnection roamingDb)
        {
            if (roamingDb == null) throw new ArgumentNullException(nameof(roamingDb));

            _roamingDb = roamingDb;
        }

        public static async Task<SearchQueryRepository> CreateAsync(SQLiteAsyncConnection roamingDb)
        {
            var searchItemRepository = new SearchQueryRepository(roamingDb);

            await searchItemRepository._roamingDb.CreateTablesAsync(typeof(SearchQuery));

            return searchItemRepository;
        }


        public async Task<IReadOnlyCollection<SearchQuery>> GetSearchHistoryAsync()
        {
            return await _roamingDb
                .Table<SearchQuery>()
                .ToListAsync();
        }
    }
}
