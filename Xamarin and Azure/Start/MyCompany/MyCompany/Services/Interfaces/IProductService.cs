using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MyCompany.Models;

namespace MyCompany.Services
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetProducts();
	}
}
