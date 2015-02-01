using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllAboutManga.Data.Libs;
using AllAboutManga.Data.Libs.Models;
using SQLite;

namespace AllAboutManga.Data.Sql
{
    public class RoamableRepository : IRoamableRepository
    {
        private readonly Lazy<SQLiteAsyncConnection> _database;

        private SQLiteAsyncConnection LocalDb
        {
            get { return _database.Value; }
        }

        public RoamableRepository(string databasePath)
        {
            if (databasePath == null) throw new ArgumentNullException(nameof(databasePath));
             _database = new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(databasePath));
        }

        public Task<IReadOnlyCollection<Manga>> GetFavorites()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Manga>> GetHistory()
        {
            throw new NotImplementedException();
        }
    }
}
