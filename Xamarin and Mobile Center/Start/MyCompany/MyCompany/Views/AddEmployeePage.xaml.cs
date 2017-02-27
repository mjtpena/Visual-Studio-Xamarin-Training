using System;
using System.Collections.Generic;
using MyCompany.ViewModels;
using Xamarin.Forms;

namespace MyCompany
{
	public partial class AddEmployeePage : ContentPage
	{
		async void SaveEmployee_Clicked(object sender, System.EventArgs e)
		{
			await ViewModel.SaveEmployee();
			await Navigation.PopAsync();
		}

		public AddEmployeeViewModel ViewModel { get { return (BindingContext as AddEmployeeViewModel); } }

		public AddEmployeePage()
		{
			InitializeComponent();
		}
	}
}
