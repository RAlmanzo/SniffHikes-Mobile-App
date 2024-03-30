using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class ZonesViewModel : FreshBasePageModel
    {
        private readonly IZonesService _zonesService;

        private ObservableCollection<Zone> zones;
        private ObservableCollection<Domain.Models.Image> images;
        private Zone selectedZone;

        public Zone SelectedZone
        {
            get => selectedZone;
            set
            {
                selectedZone = value;

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

        public ObservableCollection<Zone> Zones
        {
            get => zones;
            set
            {
                zones = value;
                RaisePropertyChanged(nameof(Zones));
            }
        }

        public ZonesViewModel()
        {
            _zonesService = new MockZonesService();
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            RefreshData.Execute(null);
        }

        public ICommand RefreshData
        {
            get
            {
                return new Command(async () =>
                {
                    //List<Domain.Models.Image> images = await _eventsService.GetEventImagesByEventIdAsync();
                    List<Zone> fetchedEvents = await _zonesService.GetAllZonesAsync();
                    Zones = new ObservableCollection<Zone>(fetchedEvents);
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
    }
}
