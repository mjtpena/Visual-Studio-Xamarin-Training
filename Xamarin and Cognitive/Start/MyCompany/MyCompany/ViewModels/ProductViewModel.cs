using MyCompany.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MyCompany.Services;

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

				IProductService productService = new ProductMockableService();

				var products = await productService.GetProducts();

				Products.Clear();
				foreach (var product in products)
					Products.Add(product);
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
