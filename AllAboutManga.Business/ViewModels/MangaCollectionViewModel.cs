using System;
using System.Collections.Generic;
using System.Linq;
using AllAboutManga.Business.Libs.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;

namespace AllAboutManga.Business.ViewModels
{
    public class MangaCollectionViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly MangaViewModelFactory _mangaViewModelFactory;

        private MangaOrder _mangaOrdering;
        private MangaFilter _mangaFilter;
        private string _searchQuery;

        private IEnumerable<Manga> _mangaCollection;
        private IEnumerable<MangaViewModel> _mangaViewModels;

        public IEnumerable<MangaViewModel> Mangas
        {
            get { return _mangaViewModels; }
            private set { SetProperty(ref _mangaViewModels, value); }
        }

        public DelegateCommand<MangaViewModel> SelectMangaCommand { get; } 

        public MangaCollectionViewModel(IEventAggregator eventAggregator,
            INavigationService navigationService,
            MangaViewModelFactory mangaViewModelFactory)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
            if (mangaViewModelFactory == null) throw new ArgumentNullException(nameof(mangaViewModelFactory));
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _mangaViewModelFactory = mangaViewModelFactory;

            _eventAggregator.GetEvent<MangaCollectionChangedEvent>().Subscribe(mangaCollection =>
            {
                _mangaCollection = mangaCollection.Mangas;
                Mangas = PrepareMangaCollection(_mangaCollection);
            });

            eventAggregator.GetEvent<MangaFilterChangedEvent>().Subscribe(mangaFilter =>
            {
                _mangaFilter = mangaFilter;
                Mangas = PrepareMangaCollection(_mangaCollection);
            });

            eventAggregator.GetEvent<MangaSearchQueryChangedEvent>().Subscribe(searchQuery =>
            {
                _searchQuery = searchQuery;
                Mangas = _searchQuery != null ?
                    _mangaViewModels.Where(mangaVm => mangaVm.Title.IndexOf(_searchQuery, StringComparison.OrdinalIgnoreCase) >= 0) :
                    PrepareMangaCollection(_mangaCollection);
            });

            eventAggregator.GetEvent<MangaOrderChangedEvent>().Subscribe(mangaOrdering =>
            {
                _mangaOrdering = mangaOrdering;
                Mangas = OrderMangas(_mangaViewModels);
            });

            SelectMangaCommand = new DelegateCommand<MangaViewModel>(mangaVm => _navigationService.Navigate("MangaPage", mangaVm.Model.Id), 
                mangaVm => mangaVm != null);
        }

        private IEnumerable<Manga> FilterMangas(IEnumerable<Manga> mangas)
        {
            if (mangas == null) throw new ArgumentNullException(nameof(mangas));

            IEnumerable<Manga> filteredMangas;
            switch (_mangaFilter)
            {
                case MangaFilter.Favorites:
                    filteredMangas = null;
                    break;
                case MangaFilter.All:
                    filteredMangas = mangas;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            };

            return filteredMangas
                .Where(manga => !string.IsNullOrWhiteSpace(manga.Image));
        }

        private IEnumerable<Manga> SearchMangas(IEnumerable<Manga> mangas)
        {
            if (mangas == null) throw new ArgumentNullException(nameof(mangas));

            // Return only mangas which contains the search query or if the query is null the entire set
            return _searchQuery != null ?
                mangas.Where(manga => manga.Title.IndexOf(_searchQuery, StringComparison.OrdinalIgnoreCase) >= 0) :
                mangas;
        }

        private IEnumerable<MangaViewModel> OrderMangas(IEnumerable<MangaViewModel> mangaViewModels)
        {
            if (mangaViewModels == null) throw new ArgumentNullException(nameof(mangaViewModels));

            switch (_mangaOrdering)
            {
                case MangaOrder.Popularity:
                    return mangaViewModels.OrderByDescending(mangaVm => mangaVm.Hits);
                case MangaOrder.Alphabetical:
                    return mangaViewModels.OrderBy(mangaVm => mangaVm.Title);
                case MangaOrder.Latest:
                    return mangaViewModels.OrderByDescending(mangaVM => mangaVM.LastChapterDate);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerable<MangaViewModel> PrepareMangaCollection(IEnumerable<Manga> rawMangas)
        {
            var mangas = FilterMangas(rawMangas);
            mangas = SearchMangas(mangas);

            var mangaViewModels = mangas
                .Select(_mangaViewModelFactory.Create)
                .ToArray();

            return OrderMangas(mangaViewModels);            
        }
    }
}
