using System;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using AllAboutManga.Business.Libs.Models;

namespace AllAboutManga.Business.ViewModels
{
    public class MangaSearchViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _searchQuery;

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                if (SetProperty(ref _searchQuery, value))
                {
                    _eventAggregator.GetEvent<MangaSearchQueryChangedEvent>().Publish(_searchQuery);
                }
            }
        }

        public MangaSearchViewModel(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator = eventAggregator;
        }
    }
}
