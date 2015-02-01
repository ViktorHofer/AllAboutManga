using System;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using AllAboutManga.Business.ViewModels;
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
using AllAboutManga.Data.Libs;
using AllAboutManga.Data.InMemory;
using AllAboutManga.Business.Libs.Profiles;
using AllAboutManga.Business.Libs.Models;
using AllAboutManga.Business.Libs.Services;
using AllAboutManga.Business.Services;
using AllAboutManga.Data.Sql;

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

            Mapper.AddProfile(new Webservice.MangaEden.Profiles.MangaCollectionProfile());
            Mapper.AddProfile(new Webservice.MangaEden.Profiles.MangaProfile());
            Mapper.AddProfile(new Webservice.MangaEden.Profiles.PageProfile());

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
                .RegisterType<Webservice.Libs.IMangaCollectionService, Webservice.MangaEden.MangaCollectionService>(new ContainerControlledLifetimeManager())
                .RegisterType<Webservice.Libs.IMangaService, Webservice.MangaEden.MangaService>(new ContainerControlledLifetimeManager())
                .RegisterType<Webservice.Libs.IPageService, Webservice.MangaEden.PageService>(new ContainerControlledLifetimeManager())
                .RegisterType<Webservice.Libs.IImageService, Webservice.MangaEden.ImageService>(new ContainerControlledLifetimeManager())

                // ViewModels
                .RegisterType<MainPageViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<MangaCollectionViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<MangaOrderViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<MangaDisplayOptionViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<MangaSearchViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<SettingsPageViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<MangaViewModelFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<ImageViewModelFactory>(new ContainerControlledLifetimeManager())

                // Services
                .RegisterType<IMangaService, MangaService>(new ContainerControlledLifetimeManager())

                // Repositories
                .RegisterType<IMangaRepository, MangaRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IRoamableRepository, RoamableRepository>(new ContainerControlledLifetimeManager())
                ;

            // Apply auto mapper profiles
            Mapper.AddProfile(new WebserviceProfile());
            Mapper.AddProfile(new DataProfile());
            await Task.FromResult<object>(null);
        }

        protected override async Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            await _unityContainer.Resolve<IMangaService>().LoadAsync();

            NavigationService.Navigate("Main", MangaDisplayOption.All);
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