using Newtonsoft.Json;

namespace eCommerce.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        [JsonProperty("picture_url")]
        public string PictureURL { get; set; }

        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }
    }
}
