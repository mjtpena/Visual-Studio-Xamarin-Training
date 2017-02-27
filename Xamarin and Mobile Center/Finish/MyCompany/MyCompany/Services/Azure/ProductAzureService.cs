using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCompany.Models;

namespace MyCompany.Services
{
	public class ProductAzureService : IProductService
	{
		public async Task<IEnumerable<Product>> GetProducts()
		{
			throw new NotImplementedException();
		}
	}
}
