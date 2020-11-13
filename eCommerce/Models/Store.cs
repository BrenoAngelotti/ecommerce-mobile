using System;
using eCommerce.Helpers;

namespace eCommerce.Models
{
    public class Store : BindableObject
    {
        public string Name { get; set; }

        public string LogoURL { get; set; }
    }
}
