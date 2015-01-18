using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Mvvm;
using Windows.UI.Xaml.Navigation;
using AllAboutManga.Business.Libs.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace AllAboutManga.Business.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public MangaBoxViewModel MangaBoxViewModel { get; }

        public MangaOrderViewModel MangaOrderViewModel { get; }

        public MangaFilterViewModel MangaFilterViewModel { get; }

        public MangaSearchViewModel MangaSearchViewModel { get; }

        public MainPageViewModel(MangaBoxViewModel mangaBoxViewModel,
            MangaOrderViewModel mangaOrderViewModel,
            MangaFilterViewModel mangaFilterViewModel,
            MangaSearchViewModel mangaSearchViewModel,
            IEventAggregator eventAggregator)
        {
            if (mangaBoxViewModel == null) throw new ArgumentNullException(nameof(mangaBoxViewModel));
            if (mangaOrderViewModel == null) throw new ArgumentNullException(nameof(mangaOrderViewModel));
            if (mangaFilterViewModel == null) throw new ArgumentNullException(nameof(mangaFilterViewModel));
            if (mangaSearchViewModel == null) throw new ArgumentNullException(nameof(mangaSearchViewModel));
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));

            MangaBoxViewModel = mangaBoxViewModel;
            MangaOrderViewModel = mangaOrderViewModel;
            MangaFilterViewModel = mangaFilterViewModel;
            MangaSearchViewModel = mangaSearchViewModel;
            _eventAggregator = eventAggregator;
        }

        public override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            if (navigationParameter == null || !(navigationParameter is MangaFilter))
                throw new ArgumentException(nameof(navigationParameter));

            _eventAggregator.GetEvent<MangaFilterChangedEvent>().Publish((MangaFilter)navigationParameter);
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }
    }
}
