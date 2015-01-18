using System;
using System.Threading;
using AllAboutManga.Webservice.Libs;
using MetroLog;

namespace AllAboutManga.Business.ViewModels
{
    public class ImageViewModelFactory
    {
        private readonly IImageService _imageService;
        private readonly ILogManager _logManager;

        public ImageViewModelFactory(IImageService imageService,
            ILogManager logManager)
        {
            if (imageService == null) throw new ArgumentNullException(nameof(imageService));
            if (logManager == null) throw new ArgumentNullException(nameof(logManager));
            _imageService = imageService;
            _logManager = logManager;
        }

        public ImageViewModel Create(string imageUrl)
        {
            if (imageUrl == null) throw new ArgumentNullException(nameof(imageUrl));

            return new ImageViewModel(_imageService,
                _logManager,
                SynchronizationContext.Current,
                imageUrl);
        }
    }
}
