using System;
using System.Collections.Generic;
using MyCompany.Models;
using MyCompany.ViewModels;
using Xamarin.Forms;

namespace MyCompany.Views
{
	public partial class EmployeePage : ContentPage
	{
		async void DeleteEmployee_Clicked(object sender, System.EventArgs e)
		{
			var mi = ((MenuItem)sender);
			var employee = mi.CommandParameter as Employee;
			await ViewModel.DeleteEmployee(employee);
		}

		async void AddEmployee_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddEmployeePage());
		}

		public EmployeePage()
		{
			InitializeComponent();
		}

		public EmployeeViewModel ViewModel { get { return (BindingContext as EmployeeViewModel); } }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (ViewModel.Employees.Count == 0)
				ViewModel.GetEmployeesCommand.Execute(null);
		}
	}
}
