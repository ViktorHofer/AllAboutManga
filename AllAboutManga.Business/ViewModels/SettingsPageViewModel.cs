using System;
using Windows.ApplicationModel;
using MetroLog;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;

namespace AllAboutManga.Business.ViewModels
{
    public class SettingsPageViewModel : BindableBase, IFlyoutViewModel
    {
        private readonly ILogger _logger;

        public string AppVersion
        {
            get
            {
                var version = Package.Current.Id.Version;
                return string.Format("{0}.{1}.{2}.{3}", 
                    version.Major, 
                    version.Minor, 
                    version.Build, 
                    version.Revision);
            }
        }

        public Action CloseFlyout { get; set; }

        public SettingsPageViewModel(ILogManager logManager)
        {
            if (logManager == null) throw new ArgumentNullException(nameof(logManager));
            _logger = logManager.GetLogger<SettingsPageViewModel>();
        }
    }
}
