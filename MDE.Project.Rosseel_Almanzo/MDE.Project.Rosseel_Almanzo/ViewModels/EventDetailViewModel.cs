using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using System.Linq;
using Newtonsoft.Json.Serialization;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using MDE.Project.Rosseel_Almanzo.Infrastructure.Services;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class EventDetailViewModel : FreshBasePageModel
    {
        private readonly IEventsService _eventsService;

        private string id;
        private string title;
        private string description;
        private string street;
        private string city;
        private string country;
        private DateTime dateEvent;
        private ObservableCollection<Domain.Models.Image> images;
        private ObservableCollection<Comment> comments;
        private Comment selectedComment;
        private bool isAdmin;
        private string userId;
        private int attendingUsers;

        public int AttendingUsers
        {
            get => attendingUsers;
            set
            {
                attendingUsers = value;
                RaisePropertyChanged(nameof(AttendingUsers));
            }
        }

        public string UserId
        {
            get => userId;
            set => userId = value;
        }

        public bool IsAdmin
        {
            get => isAdmin;
            set
            {
                isAdmin = value;
                RaisePropertyChanged(nameof(IsAdmin));
            }
        }

        public Comment SelectedComment
        {
            get => selectedComment;
            set
            {
                selectedComment = value;
                RaisePropertyChanged(nameof(SelectedComment));
                if (selectedComment != null)
                {
                    DeleteCommentCommand.Execute(null);
                }
            }
        }

        public ObservableCollection<Comment> Comments
        {
            get => comments;
            set
            {
                comments = value;
                RaisePropertyChanged(nameof(Comments));
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

        public DateTime DateEvent
        {
            get => dateEvent;
            set
            {
                dateEvent = value;
                RaisePropertyChanged(nameof(DateEvent));
            }
        }

        public string Country
        {
            get => country;
            set
            {
                country = value;
                RaisePropertyChanged(nameof(Country));
            }
        }

        public string City
        {
            get => city;
            set
            {
                city = value;
                RaisePropertyChanged(nameof(City));
            }
        }

        public string Street
        {
            get => street;
            set
            {
                street = value;
                RaisePropertyChanged(nameof(Street));
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public string Id
        {
            get => id;
            set
            {
                id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        public EventDetailViewModel(IEventsService eventsService)
        {
            _eventsService = eventsService;
            Images = new ObservableCollection<Domain.Models.Image>();
            Comments = new ObservableCollection<Comment>();
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            Id = initData.ToString();

            string admin = await SecureStorage.GetAsync("admin");
            IsAdmin = bool.Parse(admin);
            userId = await SecureStorage.GetAsync("token");

            GetEventDetails.Execute(null);
        }

        public ICommand GetEventDetails
        {
            get
            {
                return new Command(async () =>
                {
                    var item = await _eventsService.GetEventByIdAsync(Id);
                    Title = item.Title;
                    Description = item.Description;
                    Street = item.Street;
                    City = item.City;
                    Country = item.Country;
                    DateEvent = item.DateEvent;
                    Images = item.Images != null ? new ObservableCollection<Domain.Models.Image>(item.Images) : new ObservableCollection<Domain.Models.Image>();
                    Comments = item.Comments != null ? new ObservableCollection<Comment>(item.Comments) : new ObservableCollection<Comment>();
                    AttendingUsers = item.AttendingUserNames.Count();
                });
            }
        }

        public ICommand GetEventDirections
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var item = await _eventsService.GetEventByIdAsync(Id);
                        var address = $"{item.Street} {item.City} {item.Country}";
                        var locations = await Geocoding.GetLocationsAsync(address);

                        var location = locations?.FirstOrDefault();
                        if (location != null)
                        {
                            var locationCoordinates = new Location(location.Latitude, location.Longitude);

                            try
                            {
                                await Map.OpenAsync(locationCoordinates, new MapLaunchOptions
                                {
                                    Name = item.Title,
                                    NavigationMode = NavigationMode.Driving,
                                });
                            }
                            catch (Exception ex)
                            {
                                // No map application available to open
                            }
                        }
                    }
                    catch (FeatureNotSupportedException fnsEx)
                    {
                        // Feature not supported on device
                    }
                    catch (Exception ex)
                    {
                        // Handle exception that may have occurred in geocoding
                    }
                });
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<EventsViewModel>();
                });
            }
        }

        public ICommand AddCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<CreateEventCommentViewModel>(id);
                });
            }
        }

        public ICommand DeleteCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (selectedComment.UserId == userId || IsAdmin)
                    {
                        var result = await CoreMethods.DisplayAlert("Delete Comment", "Are u sure u want to delete comment?", "Yes", "Cancel");
                        if (result)
                        {
                            var deleteResult = await _eventsService.DeleteCommentAsync(Id, selectedComment.Id);
                            if (deleteResult)
                            {
                                Comments.Remove(selectedComment);
                                await CoreMethods.DisplayAlert("Delete Comment", "Comment succesfull deleted", "Ok");
                            }
                            else
                            {
                                await CoreMethods.DisplayAlert("Delete Comment", "Delete comment failed!", "Ok");
                            }
                        }
                    }
                });
            }
        }

        public ICommand DeleteEventCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await _eventsService.DeleteEventAsync(Id);
                    if (result == "Deleted")
                    {
                        await CoreMethods.DisplayAlert("Deleted", "Event succesfull deleted!", "Ok");
                    }
                    else
                    {
                        await CoreMethods.DisplayAlert("Failed", result, "Ok");
                    }

                    await CoreMethods.PushPageModel<EventsViewModel>();
                });
            }
        }

        public ICommand SignUpCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isSigned = await _eventsService.SignUpToEvent(Id, userId);
                    if (isSigned)
                    {
                        AttendingUsers++;
                        await CoreMethods.DisplayAlert("Succes", "You are succesfully signed up for the event", "Ok");
                    }                  
                });
            }
        }
    }
}
