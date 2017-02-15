using System;
using System.Collections.Generic;
using MyCompany.ViewModels;
using Xamarin.Forms;

namespace MyCompany.Views
{
	public partial class EmployeePage : ContentPage
	{
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
