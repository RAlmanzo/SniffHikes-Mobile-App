using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Validators;
using MDE.Project.Rosseel_Almanzo.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class LoginViewModel : FreshBasePageModel
    {
        private const string ISLOGGED = "islogged";
        private readonly IAccountService _accountService;

        public LoginViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        private string email;
        private string password;
        private string emailError;
        private string passwordError;

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

        public string Password
        {
            get => password;
            set
            {
                password = value;
                RaisePropertyChanged(nameof(Password));
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

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var user = new User
                    {
                        Email = Email,
                        Password = Password,
                    };

                    if (Validate(user))
                    {
                        var token = await _accountService.Login(user.Email, user.Password);

                        if(token == "Email confirmed")
                        {
                            var isLogged = true;
                            Application.Current.Properties[ISLOGGED] = isLogged;
                            await CoreMethods.PushPageModel<HomeViewModel>();
                        }
                        else
                        {
                            await CoreMethods.DisplayAlert("Error", token, "ok");
                        }                     
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
                    await CoreMethods.PushPageModel<RegisterViewModel>();
                });
            }
        }

        private bool Validate(User user)
        {

            var validator = new LoginValidator();

            var result = validator.Validate(user);

            foreach (var error in result.Errors)
            {

                if (error.PropertyName == nameof(Email))
                {
                    EmailError = error.ErrorMessage;
                }

                if (error.PropertyName == nameof(Password))
                {
                    PasswordError = error.ErrorMessage;
                }
            }
            return result.IsValid;
        }
    }
}
