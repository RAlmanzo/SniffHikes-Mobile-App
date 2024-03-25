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
        private readonly IEventsService _eventsService;

        private ObservableCollection<Event> zones;
        private ObservableCollection<Domain.Models.Image> images;
        //private Event selectedEvent;

        public ObservableCollection<Domain.Models.Image> Images
        {
            get => images;
            set
            {
                images = value;
                RaisePropertyChanged(nameof(Images));
            }
        }

        public ObservableCollection<Event> Zones
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
            _eventsService = new MockEventsService();
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
                    List<Event> fetchedEvents = await _eventsService.GetAllEvents();
                    Zones = new ObservableCollection<Event>(fetchedEvents);
                });
            }
        }
    }
}
