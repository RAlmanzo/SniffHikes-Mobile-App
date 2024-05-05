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
        private string id;

        public string Id
        {
            get => id;
            set => id = value;
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

        public BaseModel SelectedEvent
        {
            get => selectedEvent;
            set
            {
                selectedEvent = value;
                if (value != null)
                {
                    if (selectedEvent.OrginazerId == id)
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

        public EventsViewModel(IEventsService eventsService)
        {
            _eventsService = eventsService;
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
                    Id = await SecureStorage.GetAsync("token");

                    var fetchedEvents = await _eventsService.GetAllEventsAsync();
                    Events = new ObservableCollection<BaseModel>(fetchedEvents);

                    var myEvents = await _eventsService.GetAllEventsByUserId(id);
                    MyEvents = new ObservableCollection<BaseModel>(myEvents);
                });
            }
        }

        public ICommand GoToUpdatePage
        {
            get
            {
                return new Command(async () =>
                {
                    if (selectedEvent != null)
                        await CoreMethods.PushPageModel<UpdateRouteViewModel>(selectedEvent.Id, false, true);
                });
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
