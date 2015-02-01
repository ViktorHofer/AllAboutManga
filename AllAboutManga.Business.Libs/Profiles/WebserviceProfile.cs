using AllAboutManga.Business.Libs.Models;
using AutoMapper;
using WebserviceModels = AllAboutManga.Webservice.Libs.Models;
using DataModels = AllAboutManga.Data.Libs.Models;

namespace AllAboutManga.Business.Libs.Profiles
{
    public class WebserviceProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<WebserviceModels.Manga, Manga>();
            CreateMap<WebserviceModels.Chapter, Chapter>();
            CreateMap<WebserviceModels.Page, Page>();

            CreateMap<WebserviceModels.Manga, DataModels.Manga>();
            CreateMap<WebserviceModels.Chapter, DataModels.Chapter>();
            CreateMap<WebserviceModels.Page, DataModels.Page>();
        }
    }
}
