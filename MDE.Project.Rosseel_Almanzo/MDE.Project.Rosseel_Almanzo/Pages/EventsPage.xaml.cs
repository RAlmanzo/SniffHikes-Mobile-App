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
    public partial class EventsPage : TabbedPage
    {
        public EventsPage()
        {
            InitializeComponent();
            CurrentPageChanged += HandleCurrentPageChanged;
        }

        private void HandleCurrentPageChanged(object sender, EventArgs e)
        {
            if (CurrentPage is CreateEventPage)
            {
                var eventsViewModel = BindingContext as EventsViewModel;
                eventsViewModel?.GoToCreateEventsPage.Execute(null);
            }
        }
    }
}