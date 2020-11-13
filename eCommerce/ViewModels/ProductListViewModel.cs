using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eCommerce.Models;
using eCommerce.Services;
using Xamarin.Forms;

namespace eCommerce.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }

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

        private string _emptyMessage;
        public string EmptyMessage
        {
            get => _emptyMessage;
            set => SetProperty(ref _emptyMessage, value);
        }


        //Bindable commands
        public ICommand LoadProductsCommand => new Command(ExecuteProductsCommand);
        public ICommand AddToCartCommand => new Command<int>(ExecuteAddToCartCommand);


        //Services
        readonly IProductService _productService;
        readonly IStoreService _storeService;
        readonly ICartService _cartService;

        public ProductListViewModel(IStoreService storeService, IProductService productService, ICartService cartService)
        {
            _storeService = storeService;
            _productService = productService;
            _cartService = cartService;

            Products = new ObservableCollection<Product>();
            Store = new Store
            {
                Name = "Carregando...",
                LogoURL = ""
            };
            Cart = new Cart();
        }


        //Commands allowing for async execution
        private async void ExecuteProductsCommand()
        {
            await GetProductsAsync();
        }

        private async void ExecuteAddToCartCommand(int id)
        {
            await AddToCartAsync(id);
        }


        //Page initializer
        public async override Task InitializeAsync()
        {
            IsBusy = true;
            if (Products.Any())
                return;
            await GetStoreAsync();
            await GetCartAsync();
            IsBusy = false;
        }


        //Async methods
        private async Task AddToCartAsync(int id)
        {
            await _cartService.Add(Products.First(p => p.Id == id));

            await GetCartAsync().ConfigureAwait(false);
        }

        private async Task GetCartAsync()
        {
            Cart = await _cartService.Get();

            //Get product data for cart entries
            Cart.Entries.ForEach(async e =>
            {
                e.Product = await _productService.GetById(e.ProductId);
            });
        }

        private async Task GetStoreAsync()
        {
            Store = await _storeService.Get().ConfigureAwait(false);
        }

        private async Task GetProductsAsync()
        {
            EmptyMessage = "Carregando...";
            var products = await _productService.Get().ConfigureAwait(false);
            
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }

            IsBusy = false;
            EmptyMessage = "Nenhum produto encontrado";
        }
    }
}
