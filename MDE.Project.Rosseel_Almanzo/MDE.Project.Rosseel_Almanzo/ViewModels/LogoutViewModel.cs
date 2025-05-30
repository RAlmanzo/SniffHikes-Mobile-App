﻿using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class LogoutViewModel : FreshBasePageModel
    {
        private const string ISLOGGED = "islogged";
        private bool isLogged = true;

        public ICommand LogoutCommand
        {
            get
            {
                return new Command(async () =>
                {
                    bool confirmed = await CoreMethods.DisplayAlert("Logout", "Are you sure you want to sign out?", "Yes", "Cancel");
                    if (confirmed)
                    {
                        var storageToken = await SecureStorage.GetAsync("token");
                        if (!string.IsNullOrWhiteSpace(storageToken))
                        {
                            SecureStorage.Remove("token");
                        }

                        if (!Application.Current.Properties.ContainsKey(ISLOGGED))
                        {
                            await CoreMethods.PushPageModel<LoginViewModel>();
                        }
                        else
                        {
                            var result = Convert.ToBoolean(Application.Current.Properties[ISLOGGED]);
                            if (Convert.ToBoolean(Application.Current.Properties[ISLOGGED]) == isLogged)
                            {
                                isLogged = false;
                                Application.Current.Properties.Remove(ISLOGGED);
                                //Application.Current.Properties[ISLOGGED] = isLogged;
                                await Application.Current.SavePropertiesAsync();
                                await CoreMethods.PushPageModel<LoginViewModel>();
                            }
                        }
                    }
                });
            }
        }
    }
}
