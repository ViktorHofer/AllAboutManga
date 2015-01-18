using System;
using Windows.Web.Http;
using System.Threading.Tasks;
using AllAboutManga.Webservice.Libs;
using AllAboutManga.Webservice.MangaEden.Models;
using AutoMapper;
using UniRock.Web;

namespace AllAboutManga.Webservice.MangaEden
{
    public class MangaService : IMangaService
    {
        private readonly IMappingEngine _mappingEngine;
        private const string MangaUrlFormat = "http://www.mangaeden.com/api/manga/{0}";

        public MangaService(IMappingEngine mappingEngine)
        {
            if (mappingEngine == null) throw new ArgumentNullException(nameof(mappingEngine));
            _mappingEngine = mappingEngine;
        }

        public async Task<Libs.Models.Manga> GetByIdAsync(string mangaId)
        {
            if (mangaId == null) throw new ArgumentNullException(nameof(mangaId));

            var url = string.Format(MangaUrlFormat, 
                mangaId);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri(url));
                response.EnsureSuccessStatusCode();

                var fullManga = await response.Content.ReadAsObjectAsync<Manga>();
                return _mappingEngine.Map<Libs.Models.Manga>(fullManga);
            }
        }

        public async Task PopulateAsync(Libs.Models.Manga manga)
        {
            if (manga == null) throw new ArgumentNullException(nameof(manga));

            var url = string.Format(MangaUrlFormat,
                manga.Id);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri(url));
                response.EnsureSuccessStatusCode();

                var fullManga = await response.Content.ReadAsObjectAsync<Manga>();
                _mappingEngine.Map(fullManga, manga);
            }
        }
    }
}
