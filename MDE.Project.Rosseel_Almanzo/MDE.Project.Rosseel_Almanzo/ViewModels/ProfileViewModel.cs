using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
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
    public class ProfileViewModel : FreshBasePageModel
    {
        private readonly IUsersService _usersService;
        private readonly IAccountService _accountService;
        private readonly IImageService _imageService;

        private string errorText;
        private string firstName;
        private string lastName;
        private string email;
        private string city;
        private string country;
        private DateTime dateOfBirth;
        private string password;
        private string gender;
        private ObservableCollection<Dog> dogs;
        private string id;
        private Domain.Models.Image image;

        public bool IsAdmin { get; set; }

        public Domain.Models.Image Image
        {
            get => image;
            set
            {
                image = value;
                RaisePropertyChanged(nameof(Image));
            }
        }

        public string Id 
        { 
            get => id;
            set
            {
                id = value;
            }
        }

        public ObservableCollection<Dog> Dogs 
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

        public string Password
        {
            get => password;
            set
            {
                password = value;
                RaisePropertyChanged(nameof(Password));
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

        public string Email
        {
            get => email;
            set
            {
                email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                RaisePropertyChanged(nameof(LastName));
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                RaisePropertyChanged(nameof(FirstName));
            }
        }

        public string ErrorText
        {
            get { return errorText; }
            set { errorText = value; RaisePropertyChanged(nameof(ErrorText)); }
        }

        public ProfileViewModel(IAccountService accountService, IUsersService usersService, IImageService imageService)
        {
            dogs = new ObservableCollection<Dog>();
            _accountService = accountService;
            _usersService = usersService;
            _imageService = imageService;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            LoadData.Execute(null);
        }

        public ICommand LoadData
        {
            get
            {
                return new Command(async () =>
                {
                    Id = await SecureStorage.GetAsync("token");
                    var currentUser = await _usersService.GetUserByIdAsync(id);
                    FirstName = currentUser.FirstName;
                    LastName = currentUser.LastName;
                    Email = currentUser.Email;
                    City = currentUser.City;
                    Country = currentUser.Country;
                    Gender = currentUser.Gender;
                    DateOfBirth = currentUser.DateOfBirth;
                    Password = currentUser.Password;
                    Dogs = currentUser.Dogs != null ? new ObservableCollection<Dog>(currentUser.Dogs) : new ObservableCollection<Dog>();
                    Image = currentUser.Image;
                    IsAdmin = currentUser.IsAdmin;
                });
            }
        }

        public ICommand UpdateProfileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var user = new User
                    {
                        Id = id,
                        FirstName = FirstName,
                        LastName = LastName,
                        Email = Email,
                        City = City,
                        Country = Country,
                        Gender = Gender,
                        DateOfBirth = DateOfBirth,
                        Password = Password,
                        Dogs = Dogs,
                        Image = Image,
                        IsAdmin = IsAdmin,
                    };

                    if (!await _usersService.UpdateUserAsync(user)) 
                    {
                        await CoreMethods.DisplayAlert("Failed", "Update profile failed please later again!", "Ok");
                    };

                    await CoreMethods.PushPageModel<ProfileViewModel>();
                });
            }
        }

        public ICommand GoToAddDogPageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<AddDogViewModel>(id);
                });
            }
        }

        public ICommand DeleteAccountCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await _accountService.DeleteAccountAsync(id);
                    if (result)
                    {
                        await CoreMethods.DisplayAlert("Deleted", "Account succesfull deleted", "Ok");
                        SecureStorage.RemoveAll();
                        await CoreMethods.PushPageModel<LoginViewModel>();
                    }
                    else
                    {
                        await CoreMethods.DisplayAlert("Failed", "Account couldn not be deleted, please contact app services", "Ok");
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
    }
}
