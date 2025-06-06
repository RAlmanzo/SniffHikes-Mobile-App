﻿using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class RegisterViewModel : FreshBasePageModel
    {
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
        private string firstNameError;
        private string lastNameError;
        private string emailError;
        private string passwordError;
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

        public string FirstNameError
        {
            get => firstNameError;
            set
            {
                firstNameError = value;
                RaisePropertyChanged(nameof(FirstNameError));
            }
        }

        public string LastNameError
        {
            get => lastNameError;
            set
            {
                lastNameError = value;
                RaisePropertyChanged(nameof(LastNameError));
            }
        }

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                RaisePropertyChanged(nameof(EmailError));
            }
        }

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                RaisePropertyChanged(nameof(PasswordError));
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

        public int Id { get; set; }

        public RegisterViewModel(IUsersService usersService, IAccountService accountService, IImageService imageService)
        {
            dateOfBirth = DateTime.Now;
            _accountService = accountService;
            _imageService = imageService;
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

        public ICommand RegisterCommand 
        {
            get
            {
                return new Command(async () =>
                {
                    var newUser = new User
                    {                       
                        FirstName = FirstName,
                        LastName = LastName,
                        Email = Email,
                        City = City,
                        Country = Country,
                        Gender = Gender,
                        DateOfBirth = DateOfBirth,
                        Password = Password,
                        Image = Image,
                    };

                    if (Validate(newUser))
                    {
                        var result = await _accountService.Register(newUser);
                        if(!result)
                        {
                            await CoreMethods.DisplayAlert("Error", "Email allready exists", "Ok");
                        }
                        else
                        {
                            await CoreMethods.DisplayAlert("Registered", "Registration completed", "Ok");
                            await CoreMethods.PushPageModel<LoginViewModel>();
                        }                       
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
                    if(Image != null)
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

        private bool Validate(User user)
        {

            var validator = new UsersValidator();

            var result = validator.Validate(user);

            foreach (var error in result.Errors)
            {
                if (error.PropertyName == nameof(FirstName))
                {
                    FirstNameError = error.ErrorMessage;
                }

                if (error.PropertyName == nameof(LastName))
                {
                    LastNameError = error.ErrorMessage;
                }

                if (error.PropertyName == nameof(Email))
                {
                    EmailError = error.ErrorMessage;
                }

                if (error.PropertyName == nameof(Password))
                {
                    PasswordError = error.ErrorMessage;
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
