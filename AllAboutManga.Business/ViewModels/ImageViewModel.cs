using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using AllAboutManga.Webservice.Libs;
using MetroLog;
using Microsoft.Practices.Prism.Mvvm;

namespace AllAboutManga.Business.ViewModels
{
    public sealed class ImageViewModel : BindableBase
    {
        private readonly IImageService _imageService;
        private readonly ILogger _logger;
        private readonly string _imageUrl;
        private readonly SynchronizationContext _synchronizationContext;
#if DEBUG
        // Simulate slow internet connection
        private static Random _random = new Random();
#endif
        private ImageSource _image;
        private Task _imageLoadingTask;

        public ImageSource Image
        {
            get
            {
                if (_image != null) return _image;
                if (_imageLoadingTask != null &&
                    (_imageLoadingTask.Status == TaskStatus.RanToCompletion || 
                    _imageLoadingTask.Status == TaskStatus.Running || 
                    _imageLoadingTask.Status == TaskStatus.WaitingForActivation ||
                    _imageLoadingTask.Status == TaskStatus.WaitingForChildrenToComplete ||
                    _imageLoadingTask.Status == TaskStatus.WaitingToRun)) return null;

                _imageLoadingTask = Task.Run(LoadImageAsync);
                return null;
            }
            private set { base.SetProperty(ref _image, value); }
        }

        public ImageViewModel(IImageService imageService,
            ILogManager logManager,
            SynchronizationContext synchronizationContext,
            string imageUrl)
        {
            if (imageService == null) throw new ArgumentNullException(nameof(imageService));
            if (logManager == null) throw new ArgumentNullException(nameof(logManager));
            if (synchronizationContext == null) throw new ArgumentNullException(nameof(synchronizationContext));
            if (imageUrl == null) throw new ArgumentNullException(nameof(imageUrl));
            _imageService = imageService;
            _logger = logManager.GetLogger<ImageViewModel>();
            _synchronizationContext = synchronizationContext;
            _imageUrl = imageUrl;
        }

        private async Task LoadImageAsync()
        {
            var imageStream = await _imageService.GetAsync(_imageUrl);
            // Skip empty streams
            if (imageStream == null || imageStream.Size <= 0) return;

            _synchronizationContext.Post(async d =>
            {
                var image = new BitmapImage();
                await image.SetSourceAsync(imageStream);
#if DEBUG
                // Simulate slow internet connection
                await Task.Delay(_random.Next(500,3000));
#endif
                Image = image;
                imageStream.Dispose();
            }, null);
        }
    }
}
