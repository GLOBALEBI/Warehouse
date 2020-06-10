using System;
using System.Collections.Generic;

namespace Warehouse.Models
{
    public partial class Shop
    {
        public Shop()
        {
            ShopProducts = new HashSet<ShopProducts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ShopTypeId { get; set; }

        public virtual ShopTypes ShopType { get; set; }
        public virtual ICollection<ShopProducts> ShopProducts { get; set; }
    }
}
