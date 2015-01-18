using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace AllAboutManga.Webservice.Libs
{
    public interface IImageService
    {
        Task<IRandomAccessStream> GetAsync(string url);
    }
}
