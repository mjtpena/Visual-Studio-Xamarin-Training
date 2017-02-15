using System;
using System.Collections.Generic;
using MyCompany.Models;
using MyCompany.ViewModels;
using Xamarin.Forms;

namespace MyCompany.Views
{
	public partial class ProductPage : ContentPage
	{
		public ProductPage()
		{
			InitializeComponent();
		}

		public ProductViewModel ViewModel { get { return (BindingContext as ProductViewModel); } }

	

		void OnSyncClicked(object sender, EventArgs e)
		{
			ViewModel?.GetProductsCommand?.Execute(null);
		}

		// Add OnItemSelected event handler here

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Product;
			if (item == null)
				return;

			await Navigation.PushAsync(new ProductDetailPage() { BindingContext = new ProductDetailViewModel(item) });

			// Manually deselect item
			ProductsListView.SelectedItem = null;
		}


		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (ViewModel.Products.Count == 0)
				ViewModel.GetProductsCommand.Execute(null);
		}
	}
}
