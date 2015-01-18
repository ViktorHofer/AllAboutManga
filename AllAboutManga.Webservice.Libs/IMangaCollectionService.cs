using System.Threading.Tasks;
using AllAboutManga.Webservice.Libs.Models;

namespace AllAboutManga.Webservice.Libs
{
    public interface IMangaCollectionService
    {
        Task<MangaCollection> GetAllAsync();

        Task<MangaCollection> GetSetAsync(uint setNumber, uint collectionLength);
    }
}
