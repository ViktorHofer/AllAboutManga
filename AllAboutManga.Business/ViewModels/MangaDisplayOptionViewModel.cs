using AllAboutManga.Business.Libs.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;

namespace AllAboutManga.Business.ViewModels
{
    public class MangaDisplayOptionViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private MangaDisplayOption _mangaDisplayOption;

        public IEnumerable<MangaDisplayOption> MangaDisplayOptions { get; }

        public MangaDisplayOption MangaDisplayOption
        {
            get { return _mangaDisplayOption; }
            set
            {
                if (SetProperty(ref _mangaDisplayOption, value))
                {
                    _eventAggregator.GetEvent<MangaDisplayOptionChangedEvent>().Publish(_mangaDisplayOption);
                }
            }
        }

        public MangaDisplayOptionViewModel(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator = eventAggregator;

            MangaDisplayOptions = Enum.GetValues(typeof(MangaDisplayOption)) as IEnumerable<MangaDisplayOption>;
        }
    }
}
