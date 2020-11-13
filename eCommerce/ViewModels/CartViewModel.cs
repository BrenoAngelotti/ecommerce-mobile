using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using eCommerce.Models;
using eCommerce.Services;
using Xamarin.Forms;

namespace eCommerce.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        //Observable for listing
        public ObservableCollection<CartEntry> Entries { get; set; }

        private Cart _cart;
        public Cart Cart
        {
            get => _cart;
            set => SetProperty(ref _cart, value);
        }

        private string _emptyMessage;
        public string EmptyMessage
        {
            get => _emptyMessage;
            set => SetProperty(ref _emptyMessage, value);
        }


        //Bindable commands
        public ICommand LoadCartCommand => new Command(ExecuteLoadCartCommand);


        //Services
        readonly ICartService _cartService;
        readonly IProductService _productService;

        public CartViewModel(ICartService cartService, IProductService productService)
        {
            _productService = productService;
            _cartService = cartService;
            
            Cart = new Cart();
            Entries = new ObservableCollection<CartEntry>();
        }

        //Commands allowing for async execution
        private async void ExecuteLoadCartCommand()
        {
            await LoadCartAsync().ConfigureAwait(false);
        }


        //Page initializer
        public async override Task InitializeAsync()
        {
            IsBusy = true;
            await LoadCartAsync();
            EmptyMessage = "Carrinho Vazio";
            IsBusy = false;
        }


        //Async methods
        private async Task LoadCartAsync()
        {
            Cart = await _cartService.Get();

            Entries.Clear();

            //Get product data for cart entries
            Cart.Entries.ForEach(async e =>
            {
                e.Product = await _productService.GetById(e.ProductId);
                Entries.Add(e);
            });
        }
    }
}
