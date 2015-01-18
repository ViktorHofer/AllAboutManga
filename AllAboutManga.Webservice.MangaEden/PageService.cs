using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;
using AllAboutManga.Webservice.MangaEden.Models;
using AutoMapper;
using UniRock.Web;

namespace AllAboutManga.Webservice.MangaEden
{
    public class PageService : Libs.IPageService
    {
        private readonly IMappingEngine _mappingEngine;
        private const string PagesUrlFormat = "http://www.mangaeden.com/api/chapter/{0}";

        public PageService(IMappingEngine mappingEngine)
        {
            if (mappingEngine == null) throw new ArgumentNullException(nameof(mappingEngine));
            _mappingEngine = mappingEngine;
        }

        public async Task<IReadOnlyCollection<Libs.Models.Page>> GetByChapterIdAsync(string chapterId)
        {
            if (chapterId == null) throw new ArgumentNullException(nameof(chapterId));

            var url = string.Format(PagesUrlFormat,
                chapterId);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri(url), HttpCompletionOption.ResponseContentRead);
                var pages = await response.Content.ReadAsObjectAsync<PageCollection>();
                
                return _mappingEngine.Map<IEnumerable<Libs.Models.Page>>(pages)
                    .ToList();
            }
        }
    }
}
