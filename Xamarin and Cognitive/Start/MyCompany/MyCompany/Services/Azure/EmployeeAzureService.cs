using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCompany.Models;

namespace MyCompany.Services
{
	public class EmployeeAzureService : IEmployeeService
	{
		public async Task AddEmployee(Employee employee)
		{
			await DataManager.DefaultManager.SaveItemAsync(employee);
		}

		public async Task DeleteEmployee(Employee employee)
		{
			await DataManager.DefaultManager.DeleteItemAsync(employee);
		}

		public async Task<IEnumerable<Employee>> GetEmployees()
		{
			return await DataManager.DefaultManager.GetItemsAsync<Employee>();
		}
	}
}
