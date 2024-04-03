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
	public partial class RegisterPage : ContentPage
	{
		//private RegisterViewModel _viewModel;

		public RegisterPage ()
		{
			InitializeComponent ();
			//_viewModel = new RegisterViewModel();
			//BindingContext = _viewModel;
		}
	}
}