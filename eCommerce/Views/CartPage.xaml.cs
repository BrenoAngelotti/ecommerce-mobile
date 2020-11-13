using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eCommerce.Helpers;
using eCommerce.Services;
using eCommerce.ViewModels;
using Xamarin.Forms;

namespace eCommerce.Views
{
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            InitializeComponent();
            var productService = DependencyService.Get<IProductService>();
            var storeService = DependencyService.Get<IStoreService>();
            var cartService = DependencyService.Get<ICartService>();
            BindingContext = new CartViewModel(storeService, productService, cartService);
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
