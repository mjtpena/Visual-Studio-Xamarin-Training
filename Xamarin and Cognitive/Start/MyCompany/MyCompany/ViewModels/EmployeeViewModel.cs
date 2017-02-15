using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using MyCompany.Services;
using MyCompany.Models;

namespace MyCompany.ViewModels
{
	public class EmployeeViewModel : ViewModelBase
	{
		public ObservableCollection<Employee> Employees { get; set; }

		public ICommand GetEmployeesCommand { get; set; }

		public EmployeeViewModel()
		{
			Employees = new ObservableCollection<Employee>();
			Title = "Employees";
			GetEmployeesCommand = RefreshCommand = new Command(
				async () => await GetEmployees(),
				() => !IsBusy);
		}

		async Task GetEmployees()
		{
			if (IsBusy)
				return;

			Exception error = null;
			try
			{
				IsBusy = true;

				IEmployeeService employeeService = new EmployeeAzureService();

				var employees = await employeeService.GetEmployees();

				Employees.Clear();
				foreach (var employee in employees)
					Employees.Add(employee);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error: " + ex);
				error = ex;
			}
			finally
			{
				IsBusy = false;
			}

			if (error != null)
				await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");
		}

		public async Task DeleteEmployee(Employee employee)
		{
			if (IsBusy)
				return;

			Exception error = null;
			try
			{
				IsBusy = true;

				IEmployeeService employeeService = new EmployeeAzureService();

				await employeeService.DeleteEmployee(employee);

				Employees.Remove(employee);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error: " + ex);
				error = ex;
			}
			finally
			{
				IsBusy = false;
			}

			if (error != null)
				await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");
		}
	}
}
