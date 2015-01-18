using AllAboutManga.Business.Libs.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;

namespace AllAboutManga.Business.ViewModels
{
    public class MangaFilterViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private MangaFilter _mangaFilter;

        public IEnumerable<MangaFilter> MangaFilterOptions { get; }

        public MangaFilter MangaFilter
        {
            get { return _mangaFilter; }
            set
            {
                if (SetProperty(ref _mangaFilter, value))
                {
                    _eventAggregator.GetEvent<MangaFilterChangedEvent>().Publish(_mangaFilter);
                }
            }
        }

        public MangaFilterViewModel(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator = eventAggregator;

            MangaFilterOptions = Enum.GetValues(typeof(MangaFilter)) as IEnumerable<MangaFilter>;
        }
    }
}
