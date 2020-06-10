using System;
using System.Collections.Generic;

namespace Warehouse.Models
{
    public partial class ShopProducts
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }

        public virtual Products Product { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
