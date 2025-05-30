﻿using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class CreateZoneViewModel : FreshBasePageModel
    {
        private readonly IZonesService _zoneService;
        private readonly IImageService _imageService;

        private string title;
        private string description;
        private string street;
        private string city;
        private string country;
        private DateTime dateEvent;
        private ObservableCollection<Domain.Models.Image> images;
        private ObservableCollection<Comment> comments;
        private string titleError;
        private string descriptionError;
        private string streetError;
        private string cityError;
        private string countryError;
        private string dateError;
        private Domain.Models.Image selectedImage;

        public Domain.Models.Image SelectedImage
        {
            get => selectedImage;
            set
            {
                selectedImage = value;
                RaisePropertyChanged(nameof(SelectedImage));
                if (selectedImage != null)
                {
                    DeleteImageCommand.Execute(null);
                }
            }
        }

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

        public string DateError
        {
            get => dateError;
            set
            {
                dateError = value;
                RaisePropertyChanged(nameof(DateError));
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

        public CreateZoneViewModel(IImageService imageService, IZonesService zoneService)
        {
            Images = new ObservableCollection<Domain.Models.Image>();
            Comments = new ObservableCollection<Comment>();
            _imageService = imageService;
            _zoneService = zoneService;
        }

        public ICommand CreateZoneCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var newZone = new Zone
                    {
                        Title = Title,
                        Description = Description,
                        Street = Street,
                        City = City,
                        Country = Country,
                        Images = Images ?? new ObservableCollection<Domain.Models.Image>(),
                        Comments = Comments ?? new ObservableCollection<Comment>(),
                    };

                    if (Validate(newZone))
                    {
                        var result = await _zoneService.CreateZoneAsync(newZone);
                        if (result == "Created")
                        {
                            await CoreMethods.DisplayAlert("Succes", "Zone succesfull created", "Ok");
                            await CoreMethods.PushPageModel<ZonesViewModel>();
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
                    else if (action == "Select picture")
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

        public ICommand CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (images.Count > 0)
                    {
                        foreach (var image in Images)
                        {
                            if (image.ImagePath != "error")
                            {
                                await _imageService.DeleteImage(image);
                            }
                        }
                    }
                    await CoreMethods.PushPageModel<ZonesViewModel>();
                });
            }
        }

        public ICommand DeleteImageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await CoreMethods.DisplayAlert("Delete image", "Are u sure u want to delete image?", "Yes", "Cancel");
                    if (result)
                    {
                        var deleteResult = await _imageService.DeleteImage(SelectedImage);
                        if (deleteResult)
                        {
                            Images.Remove(SelectedImage);
                            await CoreMethods.DisplayAlert("Delete image", "Image succesfull deleted", "Ok");
                        }
                        else
                        {
                            await CoreMethods.DisplayAlert("Delete image", "Delete image failed!", "Ok");
                        }
                    }
                });
            }
        }

        private bool Validate(Zone currentZone)
        {

            var validator = new ZonesValidator();

            var result = validator.Validate(currentZone);

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

                if (error.PropertyName == nameof(DateEvent))
                {
                    DateError = error.ErrorMessage;
                }
            }
            return result.IsValid;
        }
    }
}
