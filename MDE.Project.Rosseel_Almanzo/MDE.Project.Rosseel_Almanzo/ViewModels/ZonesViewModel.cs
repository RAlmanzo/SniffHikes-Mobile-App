using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using Xamarin.Essentials;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class ZonesViewModel : FreshBasePageModel
    {
        private readonly IZonesService _zonesService;

        private ObservableCollection<BaseModel> zones;
        private ObservableCollection<Domain.Models.Image> images;
        private BaseModel selectedZone;
        private string id;
        private bool isAdmin;

        public bool IsAdmin
        {
            get => isAdmin;
            set
            {
                isAdmin = value;
                RaisePropertyChanged(nameof(IsAdmin));
            }
        }

        public string Id
        {
            get => id;
            set => id = value;
        }

        public BaseModel SelectedZone
        {
            get => selectedZone;
            set
            {
                selectedZone = value;
                if (value != null)
                {
                    if (selectedZone.OrginazerId == id)
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

        public ObservableCollection<BaseModel> Zones
        {
            get => zones;
            set
            {
                zones = value;
                RaisePropertyChanged(nameof(Zones));
            }
        }

        public ZonesViewModel(IZonesService zonesService)
        {
            _zonesService = zonesService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            //RefreshData.Execute(null);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            RefreshData.Execute(null);
        }

        public ICommand RefreshData
        {
            get
            {
                return new Command(async () =>
                {
                    Id = await SecureStorage.GetAsync("token");
                    string admin = await SecureStorage.GetAsync("admin");
                    IsAdmin = bool.Parse(admin);

                    var fetchedEvents = await _zonesService.GetAllZonesAsync();
                    Zones = new ObservableCollection<BaseModel>(fetchedEvents);
                });
            }
        }

        public ICommand GoToDetailPage
        {
            get
            {
                return new Command(async () =>
                {
                    if (SelectedZone != null)
                        await CoreMethods.PushPageModel<ZoneDetailsViewModel>(SelectedZone.Id, false, true);
                });
            }
        }

        public ICommand GoToUpdatePage
        {
            get
            {
                return new Command(async () =>
                {
                    if (selectedZone != null)
                        await CoreMethods.PushPageModel<UpdateZoneViewModel>(selectedZone.Id, false, true);
                });
            }
        }

        public ICommand CreateZoneCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<CreateZoneViewModel>();
                });
            }
        }
    }
}
