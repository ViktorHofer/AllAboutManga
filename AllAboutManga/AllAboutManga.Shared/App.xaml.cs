using System;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using AllAboutManga.Business.ViewModels;
using AllAboutManga.Webservice.Libs;
using AllAboutManga.Webservice.MangaEden;
using AllAboutManga.Webservice.MangaEden.Profiles;
using AutoMapper;
using MetroLog;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using UniRock.Services;
using UniRock.Services.Interfaces;
#if WINDOWS_APP
using Windows.UI.ApplicationSettings;
using System.Collections.Generic;
#endif
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using AllAboutManga.DataAccess.Libs;
using AllAboutManga.DataAccess.Mock;
using AllAboutManga.Business.Libs.Profiles;
using AllAboutManga.Business.Libs.Models;

namespace AllAboutManga
{
    public sealed partial class App
    {
        private readonly IUnityContainer _unityContainer = new UnityContainer();

        public App()
        {
            InitializeComponent();
            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Trace, LogLevel.Fatal, new MetroLog.Targets.FileStreamingTarget());
            GlobalCrashHandler.Configure();
        }

        protected override object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }

        protected override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewModelTypeName = string.Format(CultureInfo.InvariantCulture, "AllAboutManga.Business.ViewModels.{0}ViewModel, AllAboutManga.Business", viewType.Name);
                var viewModelType = Type.GetType(viewModelTypeName);
                return viewModelType;
            });

            Mapper.AddProfile(new MangaCollectionProfile());
            Mapper.AddProfile(new MangaProfile());
            Mapper.AddProfile(new PageProfile());

            _unityContainer
                // Services
                .RegisterInstance(NavigationService, new ContainerControlledLifetimeManager())
                .RegisterInstance(SessionStateService, new ContainerControlledLifetimeManager())
                .RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()),
                    new ContainerControlledLifetimeManager())
                .RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager())
                .RegisterType<IAlertMessageService, AlertMessageService>(new ContainerControlledLifetimeManager())
                .RegisterInstance(LogManagerFactory.DefaultLogManager, new ContainerControlledLifetimeManager())

                // AutoMapper
                .RegisterInstance(Mapper.Engine)

                // WebServices
                .RegisterType<IMangaCollectionService, MangaCollectionService>(new ContainerControlledLifetimeManager())
                .RegisterType<IMangaService, MangaService>(new ContainerControlledLifetimeManager())
                .RegisterType<IPageService, PageService>(new ContainerControlledLifetimeManager())
                .RegisterType<IImageService, ImageService>(new ContainerControlledLifetimeManager())

                // ViewModels
                .RegisterType<MainPageViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<MangaCollectionViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<MangaOrderViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<MangaFilterViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<MangaSearchViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<SettingsPageViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<MangaViewModelFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<ImageViewModelFactory>(new ContainerControlledLifetimeManager())

                // Repositories
                .RegisterType<IMangaRepository, MangaRepository>(new ContainerControlledLifetimeManager())
                ;

            // Apply auto mapper profiles
            Mapper.AddProfile(new WebserviceProfile());
            Mapper.AddProfile(new DataAccessProfile());
            await Task.FromResult<object>(null);
        }

        protected override async Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            _unityContainer.Resolve<MangaCollectionViewModel>();
            var mangaCollectionService = _unityContainer.Resolve<IMangaCollectionService>();
            var eventAggregator = _unityContainer.Resolve<IEventAggregator>();

            // Retrieve mangas and notify
            var webserviceMangas = await mangaCollectionService.GetAllAsync();
            var businessMangas = Mapper.Map<Business.Libs.Models.MangaCollection>(webserviceMangas);
            eventAggregator.GetEvent<MangaCollectionChangedEvent>().Publish(businessMangas);

            // Save mangas into repository
            var mangaRepository = _unityContainer.Resolve<IMangaRepository>();
            var hasMangas = await mangaRepository.Any();
            if (!hasMangas)
            {
                var dataAccessMangas = Mapper.Map<DataAccess.Libs.Models.MangaCollection>(businessMangas);
                await mangaRepository.AddAll(dataAccessMangas.Mangas);
            }

            NavigationService.Navigate("Main", MangaFilter.All);
        }

#if WINDOWS_APP
        protected override IList<SettingsCommand> GetSettingsCommands()
        {
            var settingsCommands = new List<SettingsCommand>();
            var resourceLoader = _unityContainer.Resolve<IResourceLoader>();

            // TODO: Fill list
            settingsCommands.Add(new SettingsCommand(Guid.NewGuid(), resourceLoader.GetString("SettingsCharm"), h => new Views.SettingsPage().ShowIndependent()));

            return settingsCommands;
        }
#endif
    }
}