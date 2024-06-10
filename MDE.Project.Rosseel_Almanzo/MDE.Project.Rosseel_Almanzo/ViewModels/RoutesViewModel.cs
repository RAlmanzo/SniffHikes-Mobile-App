using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using System.Linq;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class RoutesViewModel : FreshBasePageModel
    {
        private readonly IRoutesService _routesService;

        private ObservableCollection<BaseModel> routes;
        private ObservableCollection<BaseModel> myRoutes;
        private ObservableCollection<Domain.Models.Image> images;
        private Domain.Models.Image image;
        private BaseModel selectedRoute;
        private string id;
        private bool isLoading;
        private bool isVisible;
        private string cityName;

        public string CityName
        {
            get => cityName;
            set
            {
                cityName = value;
                SearchByCityName();
                RaisePropertyChanged(nameof(CityName));
            }
        }

        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                RaisePropertyChanged(nameof(IsVisible));
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                RaisePropertyChanged(nameof(IsLoading));
            }
        }

        public string Id 
        {
            get => id;
            set => id = value;
        }

        public ObservableCollection<BaseModel> MyRoutes
        {
            get => myRoutes;
            set
            {
                myRoutes = value;
                RaisePropertyChanged(nameof(MyRoutes));
            }
        }

        public Domain.Models.Image Image
        {
            get => image;
            set
            {
                image = Images.FirstOrDefault();
                RaisePropertyChanged(nameof(Image));
            }
        }

        public BaseModel SelectedRoute
        {
            get => selectedRoute;
            set
            {
                selectedRoute = value;
                if (value != null)
                {
                    if (selectedRoute.OrginazerId == id)
                    {
                        GoToUpdatePage.Execute(null);
                    }
                    else
                    {
                        GoToDetailPage.Execute(null);
                    }
                }                  
            }
        }

        public ObservableCollection<Domain.Models.Image> Images
        {
            get => images;
            set
            {
                images = value;
                RaisePropertyChanged(nameof(Images));
            }
        }

        public ObservableCollection<BaseModel> Routes
        {
            get => routes;
            set
            {
                routes = value;
                RaisePropertyChanged(nameof(Routes));
            }
        }

        public RoutesViewModel(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            RefreshData.Execute(null);
        }

        public ICommand GoToUpdatePage
        {
            get
            {
                return new Command(async () =>
                {
                    if (SelectedRoute != null)
                        await CoreMethods.PushPageModel<UpdateRouteViewModel>(SelectedRoute.Id, false, true);
                });
            }
        }

        public ICommand GoToDetailPage
        {
            get
            {
                return new Command(async () =>
                {
                    if (SelectedRoute != null)
                        await CoreMethods.PushPageModel<RouteDetailsViewModel>(SelectedRoute.Id, false, true);
                });
            }
        }

        public ICommand RefreshData
        {
            get
            {
                return new Command(async () =>
                {
                    IsLoading = true;
                    IsVisible = false;

                    Id = await SecureStorage.GetAsync("token");

                    var fetchedRoutes = await _routesService.GetAllRoutesAsync();
                    Routes = new ObservableCollection<BaseModel>(fetchedRoutes);

                    var myRoutes = await _routesService.GetAllRoutesByUserId(id);
                    MyRoutes = new ObservableCollection<BaseModel>(myRoutes);

                    await Task.Delay(500);
                    IsLoading = false;
                    IsVisible = true;
                });
            }
        }

        public ICommand GoToCreateRoutePage
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<CreateRouteViewModel>();
                });
            }
        }

        private async void SearchByCityName()
        {
            var fetchedItems = await _routesService.SearchByCity(CityName);
            Routes = new ObservableCollection<BaseModel>(fetchedItems);
        }
    }
}
