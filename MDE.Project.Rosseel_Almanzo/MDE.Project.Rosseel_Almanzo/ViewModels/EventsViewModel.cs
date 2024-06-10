using FluentValidation.Validators;
using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using MDE.Project.Rosseel_Almanzo.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private bool isLoading;
        private bool isVisible;
        private string cityName;
        private bool created;
        private bool registered;
        private string userId;

        public string UserId
        {
            get => userId;
            set => userId = value;
        }

        public bool Registered
        {
            get => registered;
            set
            {
                registered = value;
                if (value)
                {
                    GetRegisteredEvents();
                }              
                RaisePropertyChanged(nameof(Registered));
            }
        }

        public bool Created
        {
            get => created;
            set 
            { 
                created = value;
                if (value)
                {
                    GetEventsByUserId();
                }             
                RaisePropertyChanged(nameof(Created));
            }
        }


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
                    IsLoading = true;
                    IsVisible = false;
                    Created = true;

                    Id = await SecureStorage.GetAsync("token");

                    var fetchedEvents = await _eventsService.GetAllEventsAsync();
                    Events = new ObservableCollection<BaseModel>(fetchedEvents);

                    GetEventsByUserId();

                    await Task.Delay(500);
                    IsLoading = false;
                    IsVisible= true;
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
                        await CoreMethods.PushPageModel<UpdateEventViewModel>(selectedEvent.Id, false, true);
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

        private async void SearchByCityName()
        {
            var fetchedItems = await _eventsService.SearchByCity(CityName);
            Events = new ObservableCollection<BaseModel>(fetchedItems);
        }

        private async void GetEventsByUserId()
        {
            Registered = false;
            var myEvents = await _eventsService.GetAllEventsByUserId(Id);
            MyEvents = new ObservableCollection<BaseModel>(myEvents);
        }

        private async void GetRegisteredEvents()
        {
            Created = false;
            var registeredEvents = await _eventsService.GetRegisteredEventsByUserId(Id);
            MyEvents = new ObservableCollection<BaseModel>(registeredEvents);
        }
    }
}
