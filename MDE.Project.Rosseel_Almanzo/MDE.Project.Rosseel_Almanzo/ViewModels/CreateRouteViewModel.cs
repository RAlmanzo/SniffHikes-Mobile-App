using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class CreateRouteViewModel : FreshBasePageModel
    {
        private readonly IRoutesService _routesService;
        private readonly IImageService _imageService;

        private string title;
        private string description;
        private string street;
        private string city;
        private string country;
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

        public CreateRouteViewModel(IRoutesService routesService, IImageService imageService)
        {
            Images = new ObservableCollection<Domain.Models.Image>();
            Comments = new ObservableCollection<Comment>();
            _routesService = routesService;
            _imageService = imageService;
        }

        public ICommand CreateRouteCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var orginazerId = await SecureStorage.GetAsync("token");
                    var newRoute = new Route
                    {
                        Title = Title,
                        Description = Description,
                        Street = Street,
                        City = City,
                        Country = Country,
                        DateEvent = DateTime.Now,
                        OrganizerId = orginazerId,
                        Images = Images ?? new ObservableCollection<Domain.Models.Image>(),
                        Comments = Comments ?? new ObservableCollection<Comment>(),
                    };

                    if (Validate(newRoute))
                    {
                        var result = await _routesService.CreateRouteAsync(newRoute);
                        if (result == "Created")
                        {
                            await CoreMethods.DisplayAlert("Succes", "Route succesfull created", "Ok");
                            await CoreMethods.PushPageModel<RoutesViewModel>();
                        }
                        else
                        {
                            await CoreMethods.DisplayAlert("Failed", result, "Ok");
                        }
                    }                 
                });
            }
        }

        public ICommand AddImageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    string action = await CoreMethods.DisplayActionSheet("Select an option", "Annuleren", null, "Take picture", "Select picture");

                    if (action == "Take picture")
                    {
                        var imageUrl = await _imageService.TakePhotoAsync();
                        var image = new Domain.Models.Image
                        {
                            ImagePath = imageUrl,
                        };
                        Images.Add(image);
                    }
                    else
                    {
                        var imageUrl = await _imageService.PickPhotoAsync();
                        var image = new Domain.Models.Image
                        {
                            ImagePath = imageUrl,
                        };
                        Images.Add(image);
                    }

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
