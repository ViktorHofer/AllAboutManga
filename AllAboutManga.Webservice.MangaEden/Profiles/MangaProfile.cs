using System.Collections.Generic;
using System.Linq;
using AllAboutManga.Webservice.MangaEden.Models;
using AutoMapper;
using UniRock;
using System;

namespace AllAboutManga.Webservice.MangaEden.Profiles
{
    public class MangaProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Manga, Libs.Models.Manga>()
                .ForMember(dest => dest.Chapters, src => src.MapFrom(s => s.Chapters
                    .Where(c => c.Count == 4)
                    .Select(ConvertToChapter)
                    .ToList()))
                .ForMember(dest => dest.LastChapterDate, src => src.MapFrom(s => s.LastChapterDate.FromEpoch()))
                .ForMember(dest => dest.Created, src => src.MapFrom(s => s.Created.FromEpoch()));
        }

        private static Libs.Models.Chapter ConvertToChapter(IEnumerable<object> c)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));

            var parts = c.ToArray();
            return new Libs.Models.Chapter
            {
                Number = (long)parts[0],
                Date = (double)parts[1],
                Title = (string)parts[2],
                Id = (string)parts[3]
            };
        }
    }
}
