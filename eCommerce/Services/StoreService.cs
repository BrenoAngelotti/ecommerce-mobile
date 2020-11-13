using System;
using System.Threading.Tasks;
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
            //Mock
            await Task.Delay(500);

            Store = new Store
            {
                Name = "Amazon",
                LogoURL = "https://upload.wikimedia.org/wikipedia/commons/d/de/Amazon_icon.png"
            };
            //End mock

            return await Task.FromResult(Store);
        }
    }
}
