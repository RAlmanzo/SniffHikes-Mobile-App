using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using FreshMvvm;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class RoutesViewModel : FreshBasePageModel
    {
        private readonly IEventsService _eventsService;

        private ObservableCollection<Event> routes;
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

        public ObservableCollection<Event> Routes
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
                    List<Event> fetchedEvents = await _eventsService.GetAllEventsAsync();
                    Routes = new ObservableCollection<Event>(fetchedEvents);
                });
            }
        }
    }
}
