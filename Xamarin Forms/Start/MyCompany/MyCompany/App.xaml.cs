using MyCompany.Views;
using Xamarin.Forms;

namespace MyCompany
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var mainPage = new TabbedPage();
			var productPage = new NavigationPage(new ProductPage()) { Title = "Products"};

			// declare EmployeePage here


			var aboutPage = new NavigationPage(new AboutPage()) { Title = "About" };

			mainPage.Children.Add(productPage);

			//add EmployeePage here to MainPage


			mainPage.Children.Add(aboutPage);


			MainPage = mainPage;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
