using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class ShopProductProcedures
    {
        WarehouseContext db = new WarehouseContext();
        public List<ShopProducts> getShopProductsInfo(int shopid)      //ასელექთებს ShopProduct ცხრილიდან ჩანაწერებს  მაღაზიის id ის  მიხედვით ის მიხედვით
        {
            List<ShopProducts> shopproduct = new List<ShopProducts>();
            shopproduct = db.ShopProducts.Where(x => x.ShopId == shopid).ToList();
            return shopproduct;
        }

        public List<ShopProducts> getShopProducts()
        {
            List<ShopProducts> getshopproducts = new List<ShopProducts>();

            getshopproducts = db.ShopProducts.ToList();
            return getshopproducts;
        }

        public void AddShopProduct(string shopid, string productname, string barcode, double price)   //ამატებს ShopProduct ცხილში ჩანაწერს
        {
            try
            {
                db.Database.BeginTransaction();
                ShopProducts shopproduct = new ShopProducts();
                int productID = db.Products.FirstOrDefault(x => x.Name == productname).Id;
                shopproduct.ShopId = Convert.ToInt32(shopid);
                shopproduct.ProductId = productID;
                shopproduct.Barcode = barcode;
                shopproduct.Price = Convert.ToDecimal(price);
                db.Add(shopproduct);
                db.SaveChanges();
                db.Database.CommitTransaction();
            }
            catch (Exception ex)
            {

                db.Database.RollbackTransaction();
                //throw new Exception(ex.Message);
            }
        }
    }
}
