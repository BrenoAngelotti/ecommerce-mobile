using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Helpers;
using eCommerce.Models;

namespace eCommerce.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> Get();

        Task<Product> GetById(int id);
    }

    public class ProductService : IProductService
    {
        static List<Product> Products = new List<Product>();

        public async Task<Product> GetById(int id)
        {
            return await Task.FromResult(Products.FirstOrDefault(p => p.Id == id));
        }

        public async Task<IEnumerable<Product>> Get()
        {
            Products.Clear();
            try
            {
                Products.AddRange(await Network.GetFromAPI<List<Product>>("products/"));
            }
            catch
            {
                Products = new List<Product>();
            }

            return await Task.FromResult(Products);
        }
    }
}
