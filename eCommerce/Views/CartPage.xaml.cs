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
            
            var cartService = DependencyService.Get<ICartService>();
            var productService = DependencyService.Get<IProductService>();

            BindingContext = new CartViewModel(cartService, productService);

            Title = "Carrinho";
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
