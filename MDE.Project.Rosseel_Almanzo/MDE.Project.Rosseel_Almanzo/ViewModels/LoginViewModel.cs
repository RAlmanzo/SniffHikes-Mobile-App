using FreshMvvm;
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
        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isLogged = true;
                    Application.Current.Properties[ISLOGGED] = isLogged;
                    await CoreMethods.PushPageModel<HomeViewModel>();
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
    }
}
