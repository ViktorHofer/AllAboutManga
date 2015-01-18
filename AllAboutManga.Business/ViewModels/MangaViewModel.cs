using System;
using AllAboutManga.Business.Libs.Models;
using MetroLog;
using Microsoft.Practices.Prism.Mvvm;

namespace AllAboutManga.Business.ViewModels
{
    public class MangaViewModel : BindableBase
    {
        private readonly ILogManager _logManager;
        internal readonly Manga Model;

        public ImageViewModel ImageViewModel { get; }

        public string Title
        {
            get { return Model.Title; }
        }

        public int Hits
        {
            get { return Model.Hits; }
        }

        public DateTime LastChapterDate
        {
            get { return Model.LastChapterDate; }
        }

        public MangaViewModel(ImageViewModelFactory imageViewModelFactory,
            ILogManager logManager,
            Manga model)
        {
            if (logManager == null) throw new ArgumentNullException(nameof(logManager));
            if (model == null) throw new ArgumentNullException(nameof(model));
            _logManager = logManager;
            Model = model;

            if (model.Image != null)
            {
                ImageViewModel = imageViewModelFactory.Create(model.Image);
            }
        }
    }
}