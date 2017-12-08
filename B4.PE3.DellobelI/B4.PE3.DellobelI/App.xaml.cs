using B4.PE3.DellobelI.Domain.Services.Abstract;
using B4.PE3.DellobelI.Domain.Services.Mock;
using B4.PE3.DellobelI.Domain.Services.SQLite;
using B4.PE3.DellobelI.ViewModels;
using FreshMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace B4.PE3.DellobelI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //FreshIOC.Container.Register<IAppSettingsService, AppSettingsInMemoryService>();
            //FreshIOC.Container.Register<IUsersService, UsersInMemoryService>();
            //FreshIOC.Container.Register<ILocationGroupsService, LocationGroupInMemoryService>();
            //FreshIOC.Container.Register<ILocationsService, LocationInMemoryService>();

            FreshIOC.Container.Register<IAppSettingsService>( new AppSettingsInMemorySQLiteService());
            FreshIOC.Container.Register<IUsersService>(new UsersSQLiteService());
            FreshIOC.Container.Register<ILocationGroupsService>(new LocationGroupsSQLiteService());
            FreshIOC.Container.Register<ILocationsService, LocationInMemoryService>();

            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainViewModel>());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
