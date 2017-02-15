using System;
using System.Collections.Generic;
using MyCompany.ViewModels;
using Xamarin.Forms;

namespace MyCompany
{
	public partial class EmployeeDetailPage : ContentPage
	{
		async void ComputeEmotion_Clicked(object sender, System.EventArgs e)
		{
			// EmotionMessage.Text	= await ViewModel.GetEmotion();
		}

		public EmployeeDetailViewModel ViewModel { get { return (BindingContext as EmployeeDetailViewModel); } }


		public EmployeeDetailPage()
		{
			InitializeComponent();
		}
	}
}
