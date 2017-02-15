using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyCompany.ViewModels
{
	public class AboutViewModel : ViewModelBase
	{
		public AboutViewModel()
		{
			Title = "About";

			OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://twitter.com/mjtpena")));
		}

		/// <summary>
		/// Command to open browser to xamarin.com
		/// </summary>
		public ICommand OpenWebCommand { get; }
	}
}
