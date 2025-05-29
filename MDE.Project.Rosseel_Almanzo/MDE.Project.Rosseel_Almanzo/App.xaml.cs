using FreshMvvm;
using Xamarin.Essentials;
using MDE.Project.Rosseel_Almanzo.Pages;
using MDE.Project.Rosseel_Almanzo.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Infrastructure.Services;
using Firebase.Database;
using Firebase.Storage;
using Firebase.Auth;

namespace MDE.Project.Rosseel_Almanzo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var firebaseClient = new FirebaseClient(Constants.Constants.DatabaseUrl);
            var firebaseStorage = new FirebaseStorage(Constants.Constants.StorageUrl);
            var firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig(Constants.Constants.ApiKey));

            // Registreren van services
            FreshIOC.Container.Register<IEventsService>(new EventsService(firebaseClient));
            FreshIOC.Container.Register<IUsersService>(new UsersService(firebaseClient));
            FreshIOC.Container.Register<IRoutesService>(new RoutesService(firebaseClient));
            FreshIOC.Container.Register<IZonesService>(new ZoneService(firebaseClient));
            FreshIOC.Container.Register<IImageService>(new ImageService(firebaseStorage));
            FreshIOC.Container.Register<IAccountService>(new AccountService(FreshIOC.Container.Resolve<IUsersService>(), firebaseAuth));

            var masterDetailNav = new FreshMasterDetailNavigationContainer();
            masterDetailNav.Init("Menu");
            masterDetailNav.AddPage<HomeViewModel>("Home", null);
            masterDetailNav.AddPage<EventsViewModel>("Events", null);
            masterDetailNav.AddPage<RoutesViewModel>("Routes", null);
            masterDetailNav.AddPage<ZonesViewModel>("Zones", null);
            masterDetailNav.AddPage<ProfileViewModel>("Profile", null);
            masterDetailNav.AddPage<LogoutViewModel>("Logout", null);
            masterDetailNav.MasterBehavior = MasterBehavior.Popover;

            MainPage = masterDetailNav;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
