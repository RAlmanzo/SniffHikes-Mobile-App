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
        private ObservableCollection<Event> myEvents;
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

        public ObservableCollection<Event> MyEvents
        {
            get => myEvents;
            set
            {
                myEvents = value;
                RaisePropertyChanged(nameof(MyEvents));
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
            myEvents = new ObservableCollection<Event>();
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
                    List<Event> fetchedEvents = await _eventsService.GetAllEventsAsync();
                    Events = new ObservableCollection<Event>(fetchedEvents);
                    //List<Event> myEvents = await _eventsService.GetAllEventsByUserId(id);
                    //Events = new ObservableCollection<Event>(fetchedEvents);
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

        public ICommand GoToCreateEventsPage
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<CreateEventViewModel>();
                });
            }
        }
    }
}
