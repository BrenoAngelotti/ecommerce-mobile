using Newtonsoft.Json;

namespace eCommerce.Models
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }

        [JsonProperty("picture")]
        public string PictureURL { get; set; }

        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }
    }
}
