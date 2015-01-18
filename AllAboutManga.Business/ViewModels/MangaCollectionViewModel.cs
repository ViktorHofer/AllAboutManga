using System;
using System.Collections.Generic;
using System.Linq;
using AllAboutManga.Business.Libs.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AllAboutManga.Business.Libs.Services;
using AllAboutManga.DataAccess.Libs;

namespace AllAboutManga.Business.ViewModels
{
    public class MangaCollectionViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly MangaViewModelFactory _mangaViewModelFactory;
        private readonly IMangaService _mangaService;
        private MangaDisplayOption _mangaDisplayOption;
        private MangaOrder _mangaOrder;
        private string _searchQuery;

        public ListCollectionView Mangas { get; private set; }

        public DelegateCommand<MangaViewModel> SelectMangaCommand { get; } 

        public MangaCollectionViewModel(IEventAggregator eventAggregator,
            INavigationService navigationService,
            IMangaService mangaService,
            MangaViewModelFactory mangaViewModelFactory)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
            if (mangaService == null) throw new ArgumentNullException(nameof(mangaService));
            if (mangaViewModelFactory == null) throw new ArgumentNullException(nameof(mangaViewModelFactory));
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _mangaService = mangaService;
            _mangaViewModelFactory = mangaViewModelFactory;

            _eventAggregator.GetEvent<MangaCollectionChangedEvent>().Subscribe(async mangaCollection =>
            {
                await ReloadList();
            });

            _eventAggregator.GetEvent<MangaDisplayOptionChangedEvent>().Subscribe(async mangaDisplayOption =>
            {
                _mangaDisplayOption = mangaDisplayOption;
                await ReloadList();
            });

            eventAggregator.GetEvent<MangaSearchQueryChangedEvent>().Subscribe(searchQuery =>
            {
                if (Mangas == null) throw new InvalidOperationException();
                _searchQuery = searchQuery;
                Mangas.Refresh();
            });

            eventAggregator.GetEvent<MangaOrderChangedEvent>().Subscribe(mangaOrder =>
            {
                if (Mangas == null) throw new InvalidOperationException();
                _mangaOrder = mangaOrder;
                Mangas.SortDescriptions.Clear();
                ApplySorting();
            });

            SelectMangaCommand = new DelegateCommand<MangaViewModel>(mangaVm => _navigationService.Navigate("MangaPage", mangaVm.Model.Id), 
                mangaVm => mangaVm != null);
        }

        private async Task ReloadList()
        {
            IEnumerable<Manga> mangaCollection;

            switch (_mangaDisplayOption)
            {
                case MangaDisplayOption.All:
                    mangaCollection = await Task.Run(() => _mangaService.GetAllAsync());
                    break;
                case MangaDisplayOption.Favorites:
                    mangaCollection = await Task.Run(() => _mangaService.GetFavoritesAsync());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Mangas = new ListCollectionView(mangaCollection
                .Select(_mangaViewModelFactory.Create)
                .ToArray());
            ApplyFilter();
            ApplySorting();
            OnPropertyChanged(nameof(Mangas));
        }

        private void ApplySorting()
        {
            switch (_mangaOrder)
            {
                case MangaOrder.Popularity:
                    Mangas.SortDescriptions.Add(new SortDescription("Hits", ListSortDirection.Descending));
                    break;
                case MangaOrder.Alphabetical:
                    Mangas.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
                    break;
                case MangaOrder.Latest:
                    Mangas.SortDescriptions.Add(new SortDescription("LastChapterDate", ListSortDirection.Descending));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }   
        }

        private void ApplyFilter()
        {
            Mangas.Filter = manga => _searchQuery != null
                ? (manga as MangaViewModel).Title.IndexOf(_searchQuery, StringComparison.OrdinalIgnoreCase) >= 0
                : true;
        }
    }
}
