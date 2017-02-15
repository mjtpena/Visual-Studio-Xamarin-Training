using MyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyCompany.ViewModels
{
	public class ProductDetailViewModel : ViewModelBase
	{
		public string ProductName { get { return SelectedProduct?.Name; } }

		public string ProductDescription { get { return SelectedProduct?.Description; } }

		public string ProductPrice { get { return SelectedProduct?.Price; } }

		public string ProductImage { get { return SelectedProduct?.Image; } }

		public bool? ProductIsSoldOut { get { return SelectedProduct?.SoldOut; } }

		public string ProductSizes { get { return $"Available Sizes: {SelectedProduct?.Options?.FirstOrDefault()?.Size} "; } }

		public string ProductColor { get { return $"Available Colors: {SelectedProduct?.Options?.FirstOrDefault()?.Color} "; } }

		public bool? ProductIsUnique { get { return SelectedProduct?.Options?.FirstOrDefault()?.OneOfAKind; } }

		Product selectedProduct;

		public Product SelectedProduct
		{
			get { return selectedProduct; }
			set { SetProperty(ref selectedProduct, value); }
		}

		public ICommand SpeakCommand { get; set; }

		public ProductDetailViewModel(Product selectedProduct = null)
		{
			SelectedProduct = selectedProduct;

		}
	}
}
