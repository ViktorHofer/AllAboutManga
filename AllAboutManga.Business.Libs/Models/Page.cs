using System;

namespace AllAboutManga.Business.Libs.Models
{
    public class Page : IEquatable<Page>
    {
        public long Number { get; set; }

        public string Image { get; set; }

        public long Height { get; set; }

        public long Width { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Page);
        }

        public override int GetHashCode()
        {
            return Image.GetHashCode();
        }

        public bool Equals(Page other)
        {
            return other?.Image == this.Image;
        }
    }
}
