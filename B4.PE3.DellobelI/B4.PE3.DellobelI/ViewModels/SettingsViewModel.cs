using B4.PE3.DellobelI.Domain.Services.Abstract;
using FreshMvvm;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE3.DellobelI.ViewModels
{
    public class SettingsViewModel : FreshBasePageModel

    {
        IAppSettingsService settingsService;
        IUsersService usersService;

        public SettingsViewModel(IAppSettingsService settingsService, IUsersService usersService)
        {
            this.settingsService = settingsService;
            this.usersService = usersService;
        }

        #region Properties

        private string username;
        public string UserName
        {
            get { return username; }
            set
            {
                username = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }

        private bool enableListSharing;
        public bool EnableListSharing
        {
            get { return enableListSharing; }
            set
            {
                enableListSharing = value;
                RaisePropertyChanged(nameof(EnableListSharing));
            }
        }

        private bool enableNotifications;
        public bool EnableNotifications
        {
            get { return enableNotifications; }
            set
            {
                enableNotifications = value;
                RaisePropertyChanged(nameof(EnableNotifications));
            }
        }

        #endregion

        /// <summary>
        /// Callled whenever the page is navigated to.
        /// </summary>
        /// <param name="initData"></param>
        public async override void Init(object initData)
        {
            base.Init(initData);

            //get settings and intialize controls
            var settings = await settingsService.GetSettings();
            EnableListSharing = settings.EnableListSharing;
            EnableNotifications = settings.EnableNotifications;

            //get current User and intialize controls
            var currentUser = await usersService.GetUserById(settings.CurrentUserId);
            UserName = currentUser.UserName;
            Email = currentUser.Email;
        }

        public ICommand SaveSettingsCommand => new Command(
            async () => {
                //save app settings
                var currentSettings = await settingsService.GetSettings();
                currentSettings.EnableListSharing = EnableListSharing;
                currentSettings.EnableNotifications = EnableNotifications;
                await settingsService.SaveSettings(currentSettings);

                //save user info settings
                var user = await usersService.GetUserById(currentSettings.CurrentUserId);
                user.UserName = UserName?.Trim();
                user.Email = Email?.Trim();
                await usersService.SaveUser(user);

                //use coremethodes to Pop pages in FreshMvvm!
                await CoreMethods.PopPageModel(false, true);
            }
        );
    }
}
