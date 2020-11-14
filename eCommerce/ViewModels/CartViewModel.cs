using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        private int _cartCount;
        public int CartCount
        {
            get => _cartCount;
            set => SetProperty(ref _cartCount, value);
        }

        private string _emptyMessage;
        public string EmptyMessage
        {
            get => _emptyMessage;
            set => SetProperty(ref _emptyMessage, value);
        }


        //Bindable commands
        public ICommand LoadCartCommand => new Command(ExecuteLoadCartCommand);
        public ICommand AddProductCommand => new Command<int>(ExecuteAddProductCommand);
        public ICommand RemoveProductCommand => new Command<int>(ExecuteRemoveProductCommand);
        public ICommand RemoveEntryCommand => new Command<int>(ExecuteRemoveEntryCommand);


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

        private async void ExecuteAddProductCommand(int id)
        {
            _cartService.Add(Entries.First(e => e.ProductId == id).Product);
            await LoadCartAsync().ConfigureAwait(false);
        }

        private async void ExecuteRemoveProductCommand(int id)
        {
            _cartService.Remove(Entries.First(e => e.ProductId == id).Product);
            await LoadCartAsync().ConfigureAwait(false);
        }

        private async void ExecuteRemoveEntryCommand(int id)
        {
            var foundEntry = Entries.First(e => e.Id == id);
            _cartService.Remove(foundEntry);
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

            CartCount = Cart.Count;
        }
    }
}
