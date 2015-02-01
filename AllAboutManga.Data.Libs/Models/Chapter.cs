using System;
using System.Collections.Generic;

namespace AllAboutManga.Data.Libs.Models
{
    public class Chapter
    {
        public long Number { get; set; }

        public double Date { get; set; }

        public string Title { get; set; }

        public string Id { get; set; }

        public IReadOnlyCollection<Page> Pages { get; set; } 
    }
}
