using AllAboutManga.Webservice.Libs.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace AllAboutManga.Business.Libs.Models
{
    public class MangaCollectionChangedEvent : PubSubEvent<MangaCollection>
    {
    }
}
