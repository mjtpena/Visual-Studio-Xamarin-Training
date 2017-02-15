using MyCompany.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyCompany.ViewModels
{
	public class EmployeeViewModel : ViewModelBase
	{
		public ObservableCollection<Employee> Employees { get; set; }

		public ICommand GetEmployeesCommand { get; set; }

		public EmployeeViewModel()
		{
			Employees = new ObservableCollection<Employee>();
			Title = "Speakers";
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

				using (var client = new HttpClient())
				{
					//grab json from server
					var json = await client.GetStringAsync("https://demo3864976.mockable.io/employee");

					//Deserialize json
					var items = JsonConvert.DeserializeObject<List<Employee>>(json);

					//Load speakers into list
					Employees.Clear();
					foreach (var item in items)
						Employees.Add(item);
				}
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
