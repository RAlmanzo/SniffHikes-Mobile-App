using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class CreateRouteViewModel : FreshBasePageModel
    {
        private readonly IRoutesService _routesService;
        private string title;
        private string description;
        private string street;
        private string city;
        private string country;
        private DateTime dateCreated;
        private ObservableCollection<Domain.Models.Image> images;
        private ObservableCollection<Comment> comments;

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

        public DateTime DateCreated
        {
            get => dateCreated;
            set
            {
                dateCreated = value;
                RaisePropertyChanged(nameof(DateCreated));
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

        public CreateRouteViewModel(IRoutesService routesService)
        {
            Images = new ObservableCollection<Domain.Models.Image>();
            Comments = new ObservableCollection<Comment>();
            _routesService = routesService;
        }

        public ICommand CreateRouteCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var routes = await _routesService.GetAllRoutesAsync();
                    var newRoute = new Route
                    {
                        Id = routes.Count + 1,
                        Title = Title,
                        Description = Description,
                        Street = Street,
                        City = City,
                        Country = Country,
                        DateEvent = DateCreated,
                        Images = Images ?? new ObservableCollection<Domain.Models.Image>(),
                        Comments = Comments ?? new ObservableCollection<Comment>(),
                    };

                    await _routesService.CreateRouteAsync(newRoute);
                    //var eventsViewModel = new EventsViewModel();
                    //eventsViewModel.MyEvents.Add(newEvent);
                    //await CoreMethods.PushPageModel<EventsViewModel>(eventsViewModel);
                    await CoreMethods.PushPageModel<RoutesViewModel>();
                });
            }
        }

        public ICommand AddImage
        {
            get
            {
                return new Command(() =>
                {

                });
            }
        }

        public ICommand RecordRouteCommand
        {
            get
            {
                return new Command(() =>
                {

                });
            }
        }
    }
}
