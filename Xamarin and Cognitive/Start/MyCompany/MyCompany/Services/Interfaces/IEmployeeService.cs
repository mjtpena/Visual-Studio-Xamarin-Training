using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCompany.Models;

namespace MyCompany.Services
{
	public interface IEmployeeService
	{
		Task<IEnumerable<Employee>> GetEmployees();

		Task AddEmployee(Employee employee);

		Task DeleteEmployee(Employee employee);
	}
}
