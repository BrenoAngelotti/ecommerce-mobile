namespace eCommerce.Models
{
    public class CartEntry : BaseEntity
    {
        public Product Product { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }

        public decimal Subtotal { get
            {
                var result = 0m;
                if(Product != null)
                {
                    result = Product.UnitPrice * Amount;
                }
                return result;
            }
        }
    }
}
