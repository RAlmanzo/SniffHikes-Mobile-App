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
    public partial class RoutesPage : TabbedPage
    {
        public RoutesPage()
        {
            InitializeComponent();
            CurrentPageChanged += HandleCurrentPageChanged;
        }

        private void HandleCurrentPageChanged(object sender, EventArgs e)
        {
            if (CurrentPage is CreateRoutePage)
            {
                var routesViewModel = BindingContext as RoutesViewModel;
                routesViewModel?.GoToCreateRoutePage.Execute(null);
            }
        }
    }
}