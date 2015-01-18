using AllAboutManga.Business.Libs.Models;
using AutoMapper;
using WebserviceModels = AllAboutManga.Webservice.Libs.Models;
using DataAccessModels = AllAboutManga.DataAccess.Libs.Models;

namespace AllAboutManga.Business.Libs.Profiles
{
    public class WebserviceProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<WebserviceModels.MangaCollection, MangaCollection>();
            CreateMap<WebserviceModels.Manga, Manga>();
            CreateMap<WebserviceModels.Chapter, Chapter>();
            CreateMap<WebserviceModels.Page, Page>();

            CreateMap<WebserviceModels.MangaCollection, DataAccessModels.MangaCollection>();
            CreateMap<WebserviceModels.Manga, DataAccessModels.Manga>();
            CreateMap<WebserviceModels.Chapter, DataAccessModels.Chapter>();
            CreateMap<WebserviceModels.Page, DataAccessModels.Page>();
        }
    }
}
