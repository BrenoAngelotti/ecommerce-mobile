using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eCommerce.Models;
using eCommerce.Services;
using eCommerce.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace eCommerce.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }

        private int _cartCount;
        public int CartCount
        {
            get => _cartCount;
            set => SetProperty(ref _cartCount, value);
        }

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
        public ICommand OpenCartCommand => new Command(ExecuteOpenCart);


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
            CartCount = 0;
        }


        //Commands allowing for async execution
        private async void ExecuteProductsCommand()
        {
            await GetDataAsync();
        }

        private async void ExecuteAddToCartCommand(int id)
        {
            await AddToCartAsync(id);
        }

        private async void ExecuteOpenCart()
        {
            await OpenCartAsync();
        }


        //Page initializer
        public async override Task InitializeAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                EmptyMessage = "Sem conexão à internet";
                IsBusy = false;
                return;
            }
            await GetCartAsync();
            if (Products.Any())
            {
                return;
            }

            IsBusy = true;
            await GetCartAsync();
            IsBusy = false;
        }


        //Async methods
        private async Task AddToCartAsync(int id)
        {
            _cartService.Add(Products.First(p => p.Id == id));

            await GetCartAsync();
        }

        private async Task GetCartAsync()
        {
            Cart = await _cartService.Get().ConfigureAwait(false);

            //Get product data for cart entries
            Cart.Entries.ForEach(async e =>
            {
                e.Product = await _productService.GetById(e.ProductId).ConfigureAwait(false);
            });

            CartCount = Cart.Count;
        }

        private async Task GetDataAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                EmptyMessage = "Sem conexão à internet";
                IsBusy = false;
                return;
            }

            EmptyMessage = "Carregando...";

            Store = await _storeService.Get().ConfigureAwait(false);
            var products = await _productService.Get().ConfigureAwait(false);
            
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }

            IsBusy = false;
            EmptyMessage = "Nenhum produto encontrado";
        }

        private async Task OpenCartAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
        }
    }
}
