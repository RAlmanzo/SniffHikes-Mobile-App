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

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class RoutesViewModel : FreshBasePageModel
    {
        private readonly IRoutesService _routesService;

        private ObservableCollection<BaseModel> routes;
        private ObservableCollection<Domain.Models.Image> images;
        private Route selectedRoute;

        public Route SelectedRoute
        {
            get => selectedRoute;
            set
            {
                selectedRoute = value;

                // RaisePropertyChanged(nameof(SelectedItem));
                GoToDetailPage.Execute(null);
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

        public RoutesViewModel()
        {
            _routesService = new RoutesService();
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            RefreshData.Execute(null);
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
                    var fetchedRoutes = await _routesService.GetAllRoutesAsync();
                    Routes = new ObservableCollection<BaseModel>(fetchedRoutes);
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
    }
}
