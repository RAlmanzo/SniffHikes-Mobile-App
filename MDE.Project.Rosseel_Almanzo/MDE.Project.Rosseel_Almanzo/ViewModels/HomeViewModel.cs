using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class HomeViewModel : FreshBasePageModel
    {
        private const string ISLOGGED = "islogged";
        private readonly bool isLogged = false;

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (!Application.Current.Properties.ContainsKey(ISLOGGED))
            {

                CoreMethods.PushPageModel<LoginViewModel>();
            }
            else
            {
                if (Convert.ToBoolean(Application.Current.Properties[ISLOGGED]) == isLogged)
                {
                    CoreMethods.PushPageModel<LoginViewModel>();
                }
            }

        }

        public ICommand HomeCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<HomeViewModel>();
                });
            }
        }
        public ICommand EventsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<EventsViewModel>();
                });
            }
        }

        public ICommand RoutesCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<RoutesViewModel>();
                });
            }
        }

        public ICommand ZonesCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<ZonesViewModel>();
                });
            }
        }
    }
}
