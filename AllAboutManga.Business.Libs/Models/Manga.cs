using System;
using System.Collections.Generic;

namespace AllAboutManga.Business.Libs.Models
{
    public class Manga : IEquatable<Manga>
    {
        public string Id { get; set; }

        public IReadOnlyCollection<object> Aka { get; set; }

        public string Alias { get; set; }

        public string Artist { get; set; }

        public string Author { get; set; }

        public IReadOnlyCollection<string> Categories { get; set; }

        public IReadOnlyCollection<Chapter> Chapters { get; set; } 

        public DateTime Created { get; set; }

        public string Description { get; set; }

        public int Hits { get; set; }

        public string Image { get; set; }

        public int Language { get; set; }

        public DateTime LastChapterDate { get; set; }

        public int Status { get; set; }

        public string Title { get; set; }

        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? default(int);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Manga);
        }

        public bool Equals(Manga other)
        {
            return other?.Id == this.Id;
        }
    }
}
