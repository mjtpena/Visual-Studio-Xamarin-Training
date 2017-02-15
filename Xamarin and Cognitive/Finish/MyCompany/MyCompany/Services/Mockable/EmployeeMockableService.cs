using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using MyCompany.Models;
using Newtonsoft.Json;

namespace MyCompany.Services
{
	public class EmployeeMockableService : IEmployeeService
	{
		public Task AddEmployee(Employee employee)
		{
			throw new NotImplementedException();
		}

		public Task DeleteEmployee(Employee employee)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Employee>> GetEmployees()
		{
			using (var client = new HttpClient())
			{
				//grab json from server
				var json = await client.GetStringAsync("https://demo3864976.mockable.io/employee");

				//Deserialize json
				return JsonConvert.DeserializeObject<List<Employee>>(json);
			}
		}
	}
}
