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
            BindingContext = new ProductListViewModel(productService);
        }
    }
}
