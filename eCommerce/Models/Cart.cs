using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Models
{
    public class Cart: BaseEntity
    {
        public List<CartEntry> Entries { get; set; }

        public decimal Total {
            get {
                return Entries.Sum(e => e.Subtotal);
            }
        }

        public int Count
        {
            get
            {
                if (Entries != null)
                    return Entries.Sum(e => e.Amount);
                else
                    return 0;
            }
        }
    }
}
