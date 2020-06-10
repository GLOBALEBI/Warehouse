using System;
using System.Collections.Generic;

namespace Warehouse.Models
{
    public partial class ShopTypes
    {
        public ShopTypes()
        {
            Shop = new HashSet<Shop>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Shop> Shop { get; set; }
    }
}
