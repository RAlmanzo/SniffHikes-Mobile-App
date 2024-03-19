using MDE.Project.Rosseel_Almanzo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDE.Project.Rosseel_Almanzo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutMenuPage : ContentPage
    {
        //private FlyoutMenuViewModel _viewModel;
        public FlyoutMenuPage()
        {
            InitializeComponent();
            //_viewModel = new FlyoutMenuViewModel();
            //BindingContext = _viewModel;
        }
    }
}