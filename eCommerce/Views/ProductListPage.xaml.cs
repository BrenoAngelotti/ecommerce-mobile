﻿using System.Threading.Tasks;
using eCommerce.Helpers;
using eCommerce.Services;
using eCommerce.ViewModels;
using Xamarin.Forms;

namespace eCommerce.Views
{
    public partial class ProductListPage : ContentPage
    {
        public ProductListPage()
        {
            InitializeComponent();
            var productService = DependencyService.Get<IProductService>();
            var storeService = DependencyService.Get<IStoreService>();
            var cartService = DependencyService.Get<ICartService>();
            BindingContext = new ProductListViewModel(storeService, productService, cartService);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await OnAppearingAsync();
        }

        async Task OnAppearingAsync()
        {
            if (BindingContext is IInitialize viewModel)
                await viewModel.InitializeAsync().ConfigureAwait(false);
        }
    }
}
