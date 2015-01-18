using AllAboutManga.Business.Libs.Models;
using AutoMapper;
using DataAccessModels = AllAboutManga.DataAccess.Libs.Models;

namespace AllAboutManga.Business.Libs.Profiles
{
    public class DataAccessProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<MangaCollection, DataAccessModels.MangaCollection>();
            CreateMap<Manga, DataAccessModels.Manga>();
            CreateMap<Chapter, DataAccessModels.Chapter>();
            CreateMap<Page, DataAccessModels.Page>();

            CreateMap<DataAccessModels.MangaCollection, MangaCollection>();
            CreateMap<DataAccessModels.Manga, Manga>();
            CreateMap<DataAccessModels.Chapter, Chapter>();
            CreateMap<DataAccessModels.Page, Page>();
        }
    }
}
