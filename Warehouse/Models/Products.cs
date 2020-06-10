using System;
using System.Collections.Generic;

namespace Warehouse.Models
{
    public partial class Products
    {
        public Products()
        {
            ShopProducts = new HashSet<ShopProducts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<ShopProducts> ShopProducts { get; set; }
    }
}
