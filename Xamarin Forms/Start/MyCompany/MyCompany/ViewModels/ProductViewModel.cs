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
	public class ProductViewModel : ViewModelBase
	{
		public ObservableCollection<Product> Products { get; set; }

		public ICommand GetProductsCommand { get; set; }

		public ProductViewModel()
		{
			Products = new ObservableCollection<Product>();
			Title = "Products";
			GetProductsCommand = RefreshCommand = new Command(
				async () => await GetProducts(),
				() => !IsBusy);
		}

		async Task GetProducts()
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
					var json = await client.GetStringAsync("https://demo3864976.mockable.io/products");

					//Deserialize json
					var items = JsonConvert.DeserializeObject<List<Product>>(json);

					//Load Products into ObservableCollection
					Products.Clear();
					foreach (var item in items)
						Products.Add(item);
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
