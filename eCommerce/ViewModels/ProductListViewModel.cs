using System;
using System.Collections.ObjectModel;
using eCommerce.Models;
using eCommerce.Services;

namespace eCommerce.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }

        readonly IProductService _productService;

        public ProductListViewModel(IProductService productService)
        {
            _productService = productService;
            Products = new ObservableCollection<Product>();
        }
    }
}
