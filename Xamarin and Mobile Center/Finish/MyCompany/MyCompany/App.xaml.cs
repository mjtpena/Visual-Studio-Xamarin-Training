using MyCompany.Views;
using Xamarin.Forms;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

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
			var employeePage = new NavigationPage(new EmployeePage()) { Title = "Employees" };

			var aboutPage = new NavigationPage(new AboutPage()) { Title = "About" };

			mainPage.Children.Add(productPage);

			//add EmployeePage here to MainPage
			mainPage.Children.Add(employeePage);

			mainPage.Children.Add(aboutPage);


			MainPage = mainPage;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			MobileCenter.Start(typeof(Analytics), typeof(Crashes));
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
