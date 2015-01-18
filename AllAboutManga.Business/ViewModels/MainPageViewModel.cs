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

        public MangaCollectionViewModel MangaCollectionViewModel { get; }

        public MangaOrderViewModel MangaOrderViewModel { get; }

        public MangaDisplayOptionViewModel MangaDisplayOptionViewModel { get; }

        public MangaSearchViewModel MangaSearchViewModel { get; }

        public MainPageViewModel(MangaCollectionViewModel mangaCollectionViewModel,
            MangaOrderViewModel mangaOrderViewModel,
            MangaDisplayOptionViewModel mangaDisplayOptionViewModel,
            MangaSearchViewModel mangaSearchViewModel,
            IEventAggregator eventAggregator)
        {
            if (mangaCollectionViewModel == null) throw new ArgumentNullException(nameof(mangaCollectionViewModel));
            if (mangaOrderViewModel == null) throw new ArgumentNullException(nameof(mangaOrderViewModel));
            if (mangaDisplayOptionViewModel == null) throw new ArgumentNullException(nameof(mangaDisplayOptionViewModel));
            if (mangaSearchViewModel == null) throw new ArgumentNullException(nameof(mangaSearchViewModel));
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));

            MangaCollectionViewModel = mangaCollectionViewModel;
            MangaOrderViewModel = mangaOrderViewModel;
            MangaDisplayOptionViewModel = mangaDisplayOptionViewModel;
            MangaSearchViewModel = mangaSearchViewModel;
            _eventAggregator = eventAggregator;
        }

        public override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            if (navigationParameter == null || !(navigationParameter is MangaDisplayOption))
                throw new ArgumentException(nameof(navigationParameter));

            _eventAggregator.GetEvent<MangaDisplayOptionChangedEvent>().Publish((MangaDisplayOption)navigationParameter);
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }
    }
}
