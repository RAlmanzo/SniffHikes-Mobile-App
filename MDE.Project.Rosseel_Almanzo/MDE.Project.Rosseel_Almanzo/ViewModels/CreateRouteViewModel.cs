using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Validators;
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
        private string titleError;
        private string descriptionError;
        private string streetError;
        private string cityError;
        private string countryError;

        public string TitleError
        {
            get => titleError;
            set
            {
                titleError = value;
                RaisePropertyChanged(nameof(TitleError));
            }
        }

        public string DescriptionError
        {
            get => descriptionError;
            set
            {
                descriptionError = value;
                RaisePropertyChanged(nameof(DescriptionError));
            }
        }

        public string StreetError
        {
            get => streetError;
            set
            {
                streetError = value;
                RaisePropertyChanged(nameof(StreetError));
            }
        }

        public string CityError
        {
            get => cityError;
            set
            {
                cityError = value;
                RaisePropertyChanged(nameof(CityError));
            }
        }

        public string CountryError
        {
            get => countryError;
            set
            {
                countryError = value;
                RaisePropertyChanged(nameof(CountryError));
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

                    if (Validate(newRoute))
                    {
                        await _routesService.CreateRouteAsync(newRoute);
                        //var eventsViewModel = new EventsViewModel();
                        //eventsViewModel.MyEvents.Add(newEvent);
                        //await CoreMethods.PushPageModel<EventsViewModel>(eventsViewModel);
                        await CoreMethods.PushPageModel<RoutesViewModel>();
                    }                 
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
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<RecordRouteViewModel>();
                });
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<RoutesViewModel>();
                });
            }
        }

        private bool Validate(Route route)
        {

            var validator = new RoutesValidator();

            var result = validator.Validate(route);

            foreach (var error in result.Errors)
            {
                if (error.PropertyName == nameof(Title))
                {
                    TitleError = error.ErrorMessage;
                }

                if (error.PropertyName == nameof(Description))
                {
                    DescriptionError = error.ErrorMessage;
                }

                if (error.PropertyName == nameof(Street))
                {
                    StreetError = error.ErrorMessage;
                }

                if (error.PropertyName == nameof(City))
                {
                    CityError = error.ErrorMessage;
                }

                if (error.PropertyName == nameof(Country))
                {
                    CountryError = error.ErrorMessage;
                }
            }
            return result.IsValid;
        }
    }
}
