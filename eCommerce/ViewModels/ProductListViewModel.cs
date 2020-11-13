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
    public class ProductListViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }

        private Store _store;
        public Store Store
        {
            get => _store;
            set => SetProperty(ref _store, value);
        }

        private string _emptyMessage;
        public string EmptyMessage
        {
            get => _emptyMessage;
            set => SetProperty(ref _emptyMessage, value);
        }

        public ICommand ProductsCommand { get; }
        public ICommand AddToCartCommand { get; }

        readonly IProductService _productService;
        readonly IStoreService _storeService;

        public ProductListViewModel(IStoreService storeService, IProductService productService)
        {
            _storeService = storeService;
            _productService = productService;
            Products = new ObservableCollection<Product>();
            Store = new Store
            {
                Name = "Carregando...",
                LogoURL = ""
            };
            ProductsCommand = new Command(ExecuteProductsCommand);
            AddToCartCommand = new Command(ExecuteAddToCartCommand);
        }

        private async void ExecuteProductsCommand()
        {
            await GetProductsAsync();
        }

        private async void ExecuteAddToCartCommand()
        {
            //await GetProductsAsync();
        }

        public async override Task InitializeAsync()
        {
            IsBusy = true;
            if (Products.Any())
                return;
            await GetStoreAsync();
            await GetProductsAsync();
            IsBusy = false;
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
