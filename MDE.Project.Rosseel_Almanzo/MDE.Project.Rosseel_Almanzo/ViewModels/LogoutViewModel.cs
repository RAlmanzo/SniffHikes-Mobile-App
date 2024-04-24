using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class LogoutViewModel : FreshBasePageModel
    {
        private const string ISLOGGED = "islogged";
        private bool isLogged = true;

        public override void Init(object initData)
        {
            base.Init(initData);

           
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            LogoutCommand.Execute(null);
        }
        public ICommand LogoutCommand
        {
            //get
            //{
            //    return new Command(async () =>
            //    {
            //        if (!Application.Current.Properties.ContainsKey(ISLOGGED))
            //        {

            //            await CoreMethods.PushPageModel<LoginViewModel>();
            //        }
            //        else
            //        {
            //            var result = Convert.ToBoolean(Application.Current.Properties[ISLOGGED]);
            //            if (Convert.ToBoolean(Application.Current.Properties[ISLOGGED]) == isLogged)
            //            {
            //                isLogged = false;
            //                //Application.Current.Properties.Remove(ISLOGGED);
            //                Application.Current.Properties[ISLOGGED] = isLogged;
            //                await CoreMethods.PushPageModel<LoginViewModel>();
            //            }
            //        }
            //    });
            //}
            get
            {
                return new Command(async () =>
                {

                    if (Application.Current.Properties.ContainsKey(ISLOGGED)
                    && Convert.ToBoolean(Application.Current.Properties[ISLOGGED]) == true)
                    {
                        Application.Current.Properties[ISLOGGED] = false;
                        await Application.Current.SavePropertiesAsync();
                    }
                });
            }
        }
    }
}
