using System;
using System.Threading.Tasks;
using Windows.Web.Http;
using AllAboutManga.Webservice.MangaEden.Models;
using AutoMapper;
using UniRock.Web;

namespace AllAboutManga.Webservice.MangaEden
{
    public class MangaCollectionService : Libs.IMangaCollectionService
    {
        private readonly IMappingEngine _mappingEngine;
        private const string PagedCollectionUrlFormat = "http://www.mangaeden.com/api/list/{0}/?p={1}&l={2}";
        private const string EntireCollectionUrlFormat = "http://www.mangaeden.com/api/list/{0}";
        private const string DefaultLanguage = "0";

        public MangaCollectionService(IMappingEngine mappingEngine)
        {
            if (mappingEngine == null) throw new ArgumentNullException(nameof(mappingEngine));
            _mappingEngine = mappingEngine;
        }

        public async Task<Libs.Models.MangaCollection> GetAllAsync()
        {
            // Prepare url for retrieving entire manga collection
            var url = string.Format(EntireCollectionUrlFormat,
                DefaultLanguage);

            return await GetAsync(url);
        }

        public async Task<Libs.Models.MangaCollection> GetSetAsync(uint setNumber, uint collectionLength)
        {
            if (collectionLength < 25 || collectionLength > 2000)
                throw new ArgumentOutOfRangeException("collectionLength", "Collection length must not be smaller than 25 or greater than 2000");

            // Prepare url for retrieving a defined set with a defined amount
            var url = string.Format(PagedCollectionUrlFormat,
                DefaultLanguage,
                setNumber,
                collectionLength);

            return await GetAsync(url);
        }

        private async Task<Libs.Models.MangaCollection> GetAsync(string url)
        {
            if (url == null) throw new ArgumentNullException("url");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri(url), HttpCompletionOption.ResponseContentRead);
                response.EnsureSuccessStatusCode();

                var mangaCollection = await response.Content.ReadAsObjectAsync<MangaCollection>();
                return _mappingEngine.Map<Libs.Models.MangaCollection>(mangaCollection);
            }
        }
    }
}
