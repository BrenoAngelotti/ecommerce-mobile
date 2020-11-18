using System;
using System.Threading.Tasks;
using eCommerce.Helpers;
using eCommerce.Models;

namespace eCommerce.Services
{
    public interface IStoreService
    {
        Task<Store> Get();
    }

    public class StoreService : IStoreService
    {
        static Store Store = new Store();

        public async Task<Store> Get()
        {
            try
            {
                Store = await Network.GetFromAPI<Store>("store/");
            }
            catch
            {
                Store = new Store { Id = 0, LogoURL = "", Name = "Loja não encontrada" };
            }

            return await Task.FromResult(Store);
        }
    }
}
