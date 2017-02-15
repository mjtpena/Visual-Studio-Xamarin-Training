using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using MyCompany.Models;
using Newtonsoft.Json;

namespace MyCompany.Services
{
	public class ProductMockableService : IProductService
	{
		public ProductMockableService()
		{
		}

		public async Task<IEnumerable<Product>> GetProducts()
		{
			using (var client = new HttpClient())
			{
				//grab json from server
				var json = await client.GetStringAsync("https://demo3864976.mockable.io/products");

				//Deserialize json
				return JsonConvert.DeserializeObject<List<Product>>(json);
			}
		}
	}
}
