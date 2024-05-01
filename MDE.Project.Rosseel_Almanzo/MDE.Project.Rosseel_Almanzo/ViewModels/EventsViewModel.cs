using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class EventsViewModel : FreshBasePageModel
    {
        private readonly IEventsService _eventsService;

        private ObservableCollection<BaseModel> events;
        private ObservableCollection<BaseModel> myEvents;
        private ObservableCollection<Domain.Models.Image> images;
        private Domain.Models.Image image;
        private BaseModel selectedEvent;

        public Domain.Models.Image Image
        {
            get => image;
            set
            {
                image = Images.FirstOrDefault();
                RaisePropertyChanged(nameof(Image));
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

        public ObservableCollection<BaseModel> MyEvents
        {
            get => myEvents;
            set
            {
                myEvents = value;
                RaisePropertyChanged(nameof(MyEvents));
            }
        }

        public ObservableCollection<BaseModel> Events 
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
            _eventsService = new EventsService();
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
                    var fetchedEvents = await _eventsService.GetAllEventsAsync();
                    Events = new ObservableCollection<BaseModel>(fetchedEvents);

                    var id = await SecureStorage.GetAsync("token");
                    var myEvents = await _eventsService.GetAllEventsByUserId(id);
                    MyEvents = new ObservableCollection<BaseModel>(myEvents);
                });
            }
        }

        public BaseModel SelectedEvent
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
