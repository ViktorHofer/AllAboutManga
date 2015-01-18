using System;
using System.Collections.Generic;
using System.Linq;
using AllAboutManga.Webservice.MangaEden.Models;
using AutoMapper;

namespace AllAboutManga.Webservice.MangaEden.Profiles
{
    public class PageProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<PageCollection, IEnumerable<Libs.Models.Page>>()
                .ConstructProjectionUsing(d => d.Pages
                    .Where(p => p.Count == 4)
                    .Select(ConvertToPage));
        }

        private static Libs.Models.Page ConvertToPage(IEnumerable<object> c)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));

            var parts = c.ToArray();
            return new Libs.Models.Page
            {
                Number = (long)parts[0],
                Image = (string)parts[1],
                Width = (long)parts[2],
                Height = (long)parts[3]
            };
        }
    }
}
