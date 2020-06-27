using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Warehouse.Models;


namespace Warehouse.Models
{
    public class ProductProcedures : PageModel
    {
        WarehouseContext db = new WarehouseContext();
       
        public void ProductAdd(string name, string company, string image_url)      //პროდუქტის დამატება
        {
            try
            {
                db.Database.BeginTransaction();
                Products product = new Products();
                product.Name = name;
                product.Company = company;
                product.ImageUrl = image_url;
                db.Add(product);
                db.SaveChanges();
                db.Database.CommitTransaction();
            }
            catch (Exception ex)
            {

                db.Database.RollbackTransaction();
                throw new Exception(ex.Message);

            }
        }

        public void ProductUpdate(int id, string name, string company, string image_url)         //პროდუქტის ძირითადი ინფორმაციის ცვლილება
        {
            try
            {


                Products product = new Products();
                product = db.Products.FirstOrDefault(x => x.Id == id);
                product.Name = name;
                product.Company = company;
                if (image_url != null)
                {
                    product.ImageUrl = image_url;
                }
                
                db.Update(product);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void ProductRemove(int id)                  //პროდუქტის წაშლა
        {
            try
            {


                db.Database.BeginTransaction();               
                //int ShopProductsID = db.ShopProducts.FirstOrDefault(x => x.ProductId == id).Id;
                var shopproduct = db.ShopProducts.Where(x => x.ProductId == id);
                if (shopproduct != null)
                {
                    foreach (var item in shopproduct)
                    {
                        db.ShopProducts.Remove(db.ShopProducts.Find(item.Id));
                    }
                }
                db.Products.Remove(db.Products.Find(id));
                db.SaveChanges();
                db.Database.CommitTransaction();
            }
            catch(Exception ex)
            {

                db.Database.RollbackTransaction();
                throw new Exception(ex.Message);
                
            }
        }

        public Products GetProductInfo(int ID)
        {
            Products product = new Products();
            product = db.Products.FirstOrDefault(x => x.Id == ID);
            return product;
        }

        public List<Products> SelectProducts()
        {
            List<Products> prod = new List<Products>();

            prod = db.Products.ToList();
            return prod;
        }

        public List<Products> searchProducts(string name=null, string company=null)
        {
            List<Products> prod = new List<Products>();
            if (name != null)
            {
                prod = db.Products.Where(x => x.Name.Contains(name)).ToList();
            }
            else 
            if (company!=null)      
            {
                prod = db.Products.Where(x => x.Company.Contains(company)).ToList();
            }
            
            return prod;
            
        }

    }
}
