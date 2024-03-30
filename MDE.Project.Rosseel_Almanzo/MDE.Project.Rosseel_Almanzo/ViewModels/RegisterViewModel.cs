using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class RegisterViewModel : FreshBasePageModel
    {
        private readonly IUsersService _usersService;

        private string errorText;
        private string firstName;
        private string lastName;
        private string email;
        private string city;
        private string country;
        private DateTime dateOfBirth;
        private string password;
        private string gender;

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

        public RegisterViewModel(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public ICommand RegisterCommand 
        {
            get
            {
                return new Command(async () =>
                {
                    var users = await _usersService.GetAllUsersAsync();
                    var newUser = new User
                    {
                        
                        Id = users.Count +1,
                        FirstName = this.FirstName,
                        LastName = LastName,
                        Email = Email,
                        City = City,
                        Country = Country,
                        Gender = Gender,
                        DateOfBirth = DateOfBirth,
                        Password = Password,
                    };

                    await _usersService.CreateUserAsync(newUser);
                    await CoreMethods.PushPageModel<LoginViewModel>();
                });
            }
        }
    }
}
