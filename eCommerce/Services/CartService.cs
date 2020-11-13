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

        Task Add(Product product);

        Task Remove(Product product);

        Task Remove(CartEntry cartEntry);

        Task Update(CartEntry cartEntry);
    }

    public class CartService : ICartService
    {
        static Cart Cart = new Cart
        {
            Entries = new List<CartEntry>()
        };

        public Task Add(Product product)
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
                    Id = Cart.Entries.Max(e => e.Id) + 1
                });
            }

            return Task.CompletedTask;
        }

        public async Task<Cart> Get()
        {
            //Mock
            await Task.Delay(500);

            Cart.Entries.Add(new CartEntry
            {
                Id = 0,
                ProductId = 1,
                Amount = 2
            }) ;

            //End mock

            return await Task.FromResult(Cart);
        }

        public Task Remove(CartEntry cartEntry)
        {
            throw new NotImplementedException();
        }

        public Task Remove(Product product)
        {
            throw new NotImplementedException();
        }

        public Task Update(CartEntry cartEntry)
        {
            throw new NotImplementedException();
        }
    }
}
