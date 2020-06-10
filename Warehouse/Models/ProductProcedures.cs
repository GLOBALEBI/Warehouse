using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class ProductProcedures
    {
        WarehouseContext db = new WarehouseContext();
        public void ProductAdd(string name, string company, string image_url)      //პროდუქტის დამატება
        {
            Products product = new Products();
            product.Name = name;
            product.Company = company;
            product.ImageUrl = image_url;
            db.Add(product);
            db.SaveChanges();
        }

        public void ProductUpdate(int id, string name, string company)         //პროდუქტის ძირითადი ინფორმაციის ცვლილება
        {
            Products product = new Products();
            product = db.Products.FirstOrDefault(x => x.Id == id);
            product.Name = name;
            product.Company = company;
            db.Update(product);
            db.SaveChanges();
        }


        public void ProductIMG(int id, string image_url)         //პროდუქტის სურათის ატვირთვა/ცვლილება
        {
            Products product = new Products();
            product = db.Products.FirstOrDefault(x => x.Id == id);
            product.ImageUrl = image_url;
            db.Update(product);
            db.SaveChanges();
        }


        public void ProductRemove(int id)                  //პროდუქტის წაშლა
        {
            int ShopProductsID = db.ShopProducts.FirstOrDefault(x => x.ProductId == id).Id;
            db.ShopProducts.Remove(db.ShopProducts.Single(x=>x.Id==ShopProductsID));
            db.Products.Remove(db.Products.Find(id));
        }
    }
}
