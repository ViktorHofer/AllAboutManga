using System;
using System.Collections.Generic;

namespace AllAboutManga.Business.Libs.Models
{
    public class Chapter : IEquatable<Chapter>
    {
        public long Number { get; set; }

        public double Date { get; set; }

        public string Title { get; set; }

        public string Id { get; set; }

        public IReadOnlyCollection<Page> Pages { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Chapter);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Chapter other)
        {
            return other?.Id == this.Id;
        }
    }
}
