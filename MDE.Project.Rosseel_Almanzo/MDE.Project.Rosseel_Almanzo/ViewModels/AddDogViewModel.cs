using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Validators;
using Xamarin.Essentials;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class AddDogViewModel : FreshBasePageModel
    {
        private readonly IUsersService _usersService;
        private readonly IImageService _imageService;

        private string name;
        private DateTime dateOfBirth;
        private string gender;
        private string race;
        private List<Dog> dogs;
        private string nameError;
        private string dateOfBirthError;
        private Domain.Models.Image image;

        public Domain.Models.Image Image
        {
            get => image;
            set
            {
                image = value;
                RaisePropertyChanged(nameof(Image));
            }
        }

        public string Id { get; private set; }

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                RaisePropertyChanged(nameof(NameError));
            }
        }

        public string DateOfBirthError
        {
            get => dateOfBirthError;
            set
            {
                dateOfBirthError = value;
                RaisePropertyChanged(nameof(DateOfBirthError));
            }
        }

        public List<Dog> Dogs
        {
            get => dogs;
            set
            {
                dogs = value;
                RaisePropertyChanged(nameof(Dogs));
            }
        }

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                RaisePropertyChanged(nameof(Gender));
            }
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                RaisePropertyChanged(nameof(DateOfBirth));
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public string Race
        {
            get => race;
            set
            {
                race = value;
                RaisePropertyChanged(nameof(Race));
            }
        }

        public AddDogViewModel(IUsersService usersService, IImageService imageService)
        {
            _usersService = usersService;
            dogs = new List<Dog>();
            _imageService = imageService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            Id = initData.ToString();
        }

        public ICommand AddDogCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var dog = new Dog
                    {
                        Name = Name,
                        Race = Race,
                        Gender = Gender,
                        DateOfBirth = DateOfBirth,
                        Image = Image,
                    };
                    
                    if (Validate(dog))
                    {
                        var result = await _usersService.AddDogAsync(Id, dog);
                        if (!result)
                        {
                            await CoreMethods.DisplayAlert("Failed", "Could not add dog!", "Ok");
                        }

                        await CoreMethods.PushPageModel<ProfileViewModel>();
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
                        Image = image;
                    }
                    else if (action == "Select picture")
                    {
                        var imageUrl = await _imageService.PickPhotoAsync();
                        var image = new Domain.Models.Image
                        {
                            ImagePath = imageUrl,
                        };
                        Image = image;
                    }
                });
            }
        }

        public ICommand DeleteImageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (Image != null)
                    {
                        var result = await CoreMethods.DisplayAlert("Delete Image", "Are u sure u want to delete image?", "Yes", "Cancel");
                        if (result)
                        {
                            var deleteResult = await _imageService.DeleteImage(image);
                            if (deleteResult)
                            {
                                Image = null;
                                await CoreMethods.DisplayAlert("Delete image", "Image succesfull deleted", "Ok");
                            }
                            else
                            {
                                await CoreMethods.DisplayAlert("Delete image", "Delete image failed!", "Ok");
                            }
                        }
                    }
                });
            }
        }

        private bool Validate(Dog dog)
        {

            var validator = new DogsValidator();

            var result = validator.Validate(dog);

            foreach (var error in result.Errors)
            {
                if (error.PropertyName == nameof(Name))
                {
                    NameError = error.ErrorMessage;
                }

                if (error.PropertyName == nameof(DateOfBirth))
                {
                    DateOfBirthError = error.ErrorMessage;
                }
            }
            return result.IsValid;
        }
    }
}
