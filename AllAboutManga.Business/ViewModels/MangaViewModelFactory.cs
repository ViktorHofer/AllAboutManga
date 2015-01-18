using System;
using AllAboutManga.Business.Libs.Models;
using MetroLog;

namespace AllAboutManga.Business.ViewModels
{
    public class MangaViewModelFactory
    {
        private readonly ImageViewModelFactory _imageViewModelFactory;
        private readonly ILogManager _logManager;

        public MangaViewModelFactory(ImageViewModelFactory imageViewModelFactory,
            ILogManager logManager)
        {
            if (imageViewModelFactory == null) throw new ArgumentNullException(nameof(imageViewModelFactory));
            if (logManager == null) throw new ArgumentNullException(nameof(logManager));
            _imageViewModelFactory = imageViewModelFactory;
            _logManager = logManager;
        }

        public MangaViewModel Create(Manga manga)
        {
            if (manga == null) throw new ArgumentNullException(nameof(manga));
            
            return new MangaViewModel(_imageViewModelFactory,
                _logManager,
                manga);
        }
    }
}
