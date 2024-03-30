using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class EventsViewModel : FreshBasePageModel
    {
        private readonly IEventsService _eventsService;

        private ObservableCollection<Event> events;
        private ObservableCollection<Domain.Models.Image> images;
        private Event selectedEvent;

        public ObservableCollection<Domain.Models.Image> Images 
        { 
            get => images;
            set
            {
                images = value;
                RaisePropertyChanged(nameof(Images));
            } 
        }

        public ObservableCollection<Event> Events 
        { 
            get => events; 
            set
            {
                events = value;
                RaisePropertyChanged(nameof(Events));
            } 
        }

        public EventsViewModel()
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
                    //Images = await _eventsService.GetEventImagesByEventIdAsync();
                    List<Event> fetchedEvents = await _eventsService.GetAllEvents();
                    Events = new ObservableCollection<Event>(fetchedEvents);
                });
            }
        }

        public Event SelectedEvent
        {
            get => selectedEvent;
            set
            {
                selectedEvent = value;

                // RaisePropertyChanged(nameof(SelectedItem));
                GoToDetailPage.Execute(null);
            }
        }

        public ICommand GoToDetailPage
        {
            get
            {
                return new Command(async () =>
                {
                    if (SelectedEvent != null)
                        await CoreMethods.PushPageModel<EventDetailViewModel>(SelectedEvent.Id, false, true);
                });
            }
        }
    }
}
