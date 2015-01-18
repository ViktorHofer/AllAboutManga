using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;
using AllAboutManga.Webservice.Libs;
using AllAboutManga.Webservice.MangaEden.Models;

namespace AllAboutManga.Webservice.MangaEden
{
    public class ImageService : IImageService
    {
        private const string ImageBaseUrlFormat = "http://cdn.mangaeden.com/mangasimg/{0}";

        public async Task<IRandomAccessStream> GetAsync(string url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            url = string.Format(ImageBaseUrlFormat,
                url);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri(url), HttpCompletionOption.ResponseContentRead);

                if (response.StatusCode == HttpStatusCode.NotFound) return null;
                response.EnsureSuccessStatusCode();

                var inMemoryRandomAccessStream = new InMemoryRandomAccessStream();
                await response.Content.WriteToStreamAsync(inMemoryRandomAccessStream);
                inMemoryRandomAccessStream.Seek(0);
                return inMemoryRandomAccessStream;
            }
        }
    }
}
