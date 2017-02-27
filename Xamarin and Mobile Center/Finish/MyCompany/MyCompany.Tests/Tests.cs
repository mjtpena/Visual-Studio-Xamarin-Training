using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MyCompany.Tests
{
	[TestFixture(Platform.Android)]
	[TestFixture(Platform.iOS)]
	public class Tests
	{
		IApp app;
		Platform platform;

		public Tests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

		[Test]
		public void AppLaunches()
		{
			app.Screenshot("First screen.");
		}

		[Test]
		public void NavigateApp()
		{
			app.Tap(x => x.Text("Awesome T-shirt"));
			app.Screenshot("Product");
			app.Tap(x => x.Text("Employees")); ;
			app.Screenshot("Employees");
			app.Tap(x => x.Text("About")); ;
			app.Screenshot("About");
			app.Tap(x => x.Text("Contact Me")); ;
		}

		[Test]
		public void AppRepl()
		{
			app.Repl();
		}

	}
}
