using System;
using System.Collections.Generic;
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

		// Add OnItemSelected event handler here

		void OnSyncClicked(object sender, EventArgs e)
		{
			ViewModel?.GetProductsCommand?.Execute(null);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (ViewModel.Products.Count == 0)
				ViewModel.GetProductsCommand.Execute(null);
		}
	}
}
