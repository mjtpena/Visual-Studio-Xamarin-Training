using System;
using System.Threading.Tasks;
using MyCompany.Models;
using MyCompany.Services;

namespace MyCompany.ViewModels
{
	public class AddEmployeeViewModel : ViewModelBase
	{
		public Employee Employee { get; set; }

		public AddEmployeeViewModel()
		{
			Employee = new Employee();
		}

		public async Task SaveEmployee()
		{
			IEmployeeService service = new EmployeeAzureService();
			await service.AddEmployee(Employee);
		}
	}
}
