using System.Collections.Generic;
using System.Threading.Tasks;
using AllAboutManga.Webservice.Libs.Models;

namespace AllAboutManga.Webservice.Libs
{
    public interface IPageService
    {
        Task<IReadOnlyCollection<Page>> GetByChapterIdAsync(string chapterId);
    }
}
