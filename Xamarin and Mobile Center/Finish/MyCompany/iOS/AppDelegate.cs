using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.Azure.Mobile;
using UIKit;

namespace MyCompany.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			//Add these for Azure Mobile Service
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			MobileCenter.Configure("43e62757-0780-4a7d-8afd-b549be9daf32");

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
