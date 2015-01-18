using AllAboutManga.Webservice.MangaEden.Models;
using AutoMapper;
using UniRock;

namespace AllAboutManga.Webservice.MangaEden.Profiles
{
    public class MangaCollectionProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<MangaMeta, Libs.Models.Manga>()
                .ForMember(dest => dest.LastChapterDate, src => src.MapFrom(s => s.LastChapterDate.FromEpoch()))
                .ForMember(dest => dest.Title, src => src.MapFrom(s => s.Title.Trim()));
            CreateMap<MangaCollection, Libs.Models.MangaCollection>();
        }
    }
}
