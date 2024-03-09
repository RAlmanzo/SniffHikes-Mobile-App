using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
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
    }
}
