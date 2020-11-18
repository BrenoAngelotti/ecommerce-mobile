using Newtonsoft.Json;

namespace eCommerce.Models
{
    public class Store : BaseEntity
    {
        public string Name { get; set; }

        [JsonProperty("logo_url")]
        public string LogoURL { get; set; }
    }
}
