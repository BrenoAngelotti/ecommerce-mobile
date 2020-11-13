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
                LogoURL = "https://www.marketplace.org/wp-content/uploads/2019/07/ama2.png?resize=740%2C204"
            };
            //End mock

            return await Task.FromResult(Store);
        }
    }
}
