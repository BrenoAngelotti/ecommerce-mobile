using eCommerce.Services;
using eCommerce.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace eCommerce
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            Register();

            var navigation = new Xamarin.Forms.NavigationPage(new ProductListPage());
            navigation.On<iOS>().SetPrefersLargeTitles(true);
            navigation.BarBackgroundColor = Color.FromHex("#f0f0f0");
            navigation.BarTextColor = Color.Black;

            MainPage = navigation;
        }

        void Register()
        {
            DependencyService.RegisterSingleton<IProductService>(new ProductService());
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
