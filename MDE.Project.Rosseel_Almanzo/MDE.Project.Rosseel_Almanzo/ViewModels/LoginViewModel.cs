using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
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

        private string email;
        private string password;
        private string emailError;
        private string passwordError;

        private bool isLoggedIn;

        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set { isLoggedIn = value; RaisePropertyChanged(nameof(IsLoggedIn)); }
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
                    // TODO set check if user email allrdy excists
                    var user = new User
                    {
                        Email = Email,
                        Password = Password,
                    };

                    if (Validate(user))
                    {
                        var isLogged = true;
                        Application.Current.Properties[ISLOGGED] = isLogged;
                        await CoreMethods.PopPageModel();
                        
                    }                 
                });
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            //Id = (int)initData;
            IsLoggedIn = !Application.Current.Properties.ContainsKey(ISLOGGED) || !Convert.ToBoolean(Application.Current.Properties[ISLOGGED]) ? false : true;

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
