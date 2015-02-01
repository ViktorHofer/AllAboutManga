using AllAboutManga.Business.Libs.Models;
using AutoMapper;
using DataAccessModels = AllAboutManga.Data.Libs.Models;

namespace AllAboutManga.Business.Libs.Profiles
{
    public class DataProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Manga, DataAccessModels.Manga>();
            CreateMap<Chapter, DataAccessModels.Chapter>();
            CreateMap<Page, DataAccessModels.Page>();

            CreateMap<DataAccessModels.Manga, Manga>();
            CreateMap<DataAccessModels.Chapter, Chapter>();
            CreateMap<DataAccessModels.Page, Page>();
        }
    }
}
