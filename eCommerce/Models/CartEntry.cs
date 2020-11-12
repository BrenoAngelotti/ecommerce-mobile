namespace eCommerce.Models
{
    public class CartEntry : BaseEntity
    {
        public Product Product { get; set; }

        public int Amount { get; set; }

        public decimal Subtotal { get
            {
                return Product.UnitPrice * Amount;
            }
        }
    }
}
