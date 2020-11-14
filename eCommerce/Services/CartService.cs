using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Models;
using Xamarin.Forms;

namespace eCommerce.Services
{
    public interface ICartService
    {
        Task<Cart> Get();

        void Add(Product product);

        void Remove(Product product);

        void Remove(CartEntry cartEntry);
    }

    public class CartService : ICartService
    {
        static Cart Cart = new Cart
        {
            Entries = new List<CartEntry>()
        };

        public void Add(Product product)
        {
            if (Cart.Entries.Any(e => e.ProductId == product.Id))
            {
                Cart.Entries.First(e => e.ProductId == product.Id).Amount++;
            }
            else
            {
                Cart.Entries.Add(new CartEntry {
                    Product = product,
                    Amount = 1,
                    ProductId = product.Id,
                    Id = Cart.Entries.Count == 0 ? 0 : Cart.Entries.Max(e => e.Id) + 1
                });
            }
        }

        public async Task<Cart> Get()
        {
            return await Task.FromResult(Cart);
        }

        public void Remove(CartEntry cartEntry)
        {
            CartEntry foundEntry = Cart.Entries.First(e => e.Id == cartEntry.Id);
            if (foundEntry == null)
            {
                return;
            }

            Cart.Entries.Remove(foundEntry);
        }

        public void Remove(Product product)
        {
            var foundEntry = Cart.Entries.First(e => e.ProductId == product.Id);
            if (foundEntry == null)
            {
                return;
            }

            if (foundEntry.Amount == 1)
            {
                Cart.Entries.Remove(foundEntry);
            }
            else
            {
                Cart.Entries.First(e => e.ProductId == product.Id).Amount--;
            }
        }
    }
}
