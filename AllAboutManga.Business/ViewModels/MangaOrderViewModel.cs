using System;
using AllAboutManga.Business.Libs.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Collections.Generic;

namespace AllAboutManga.Business.ViewModels
{
    public class MangaOrderViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private MangaOrder _mangaOrder;

        public IEnumerable<MangaOrder> MangaOrderOptions { get; }

        public MangaOrder MangaOrder
        {
            get { return _mangaOrder; }
            set
            {
                if (SetProperty(ref _mangaOrder, value))
                {
                    _eventAggregator.GetEvent<MangaOrderChangedEvent>().Publish(_mangaOrder);
                }
            }
        }

        public MangaOrderViewModel(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator = eventAggregator;

            MangaOrderOptions = Enum.GetValues(typeof(MangaOrder)) as IEnumerable<MangaOrder>;
        }
    }
}
