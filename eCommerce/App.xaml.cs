using eCommerce.Services;
using eCommerce.Views;
using Xamarin.Forms;

namespace eCommerce
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            Register();

            MainPage = new Xamarin.Forms.NavigationPage(new ProductListPage());
        }

        void Register()
        {
            DependencyService.RegisterSingleton<IProductService>(new ProductService());
            DependencyService.RegisterSingleton<IStoreService>(new StoreService());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
