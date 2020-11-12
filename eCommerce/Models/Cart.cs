using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Models
{
    public class Cart
    {
        public List<CartEntry> Entries { get; set; }

        public decimal Total {
            get {
                return Entries.Sum(e => e.Subtotal);
            }
        }
    }
}
