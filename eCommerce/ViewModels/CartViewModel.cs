using System;
using eCommerce.Models;
using eCommerce.Services;

namespace eCommerce.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private Store _store;
        public Store Store
        {
            get => _store;
            set => SetProperty(ref _store, value);
        }

        private Cart _cart;
        public Cart Cart
        {
            get => _cart;
            set => SetProperty(ref _cart, value);
        }

        readonly IProductService _productService;
        readonly IStoreService _storeService;
        readonly ICartService _cartService;

        public CartViewModel(IStoreService storeService, IProductService productService, ICartService cartService)
        {
            _storeService = storeService;
            _productService = productService;
            _cartService = cartService;
            
            Store = new Store
            {
                Name = "Carregando...",
                LogoURL = ""
            };
            Cart = new Cart();
        }
    }
}
