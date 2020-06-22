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
    public class ShopProcedures : PageModel
    {
        WarehouseContext db = new WarehouseContext();

        public void ShopAdd(string name, string Address, int ShopType)      
        {
            Shop shop = new Shop();
            shop.Name = name;
            shop.Address = Address;
            shop.ShopTypeId = ShopType;
            db.Add(shop);
            db.SaveChanges();
        }

        public ShopTypes GetShopType(string type)
        {
            ShopTypes ShopTypes = new ShopTypes();
            ShopTypes = db.ShopTypes.FirstOrDefault(x => x.Type == type);
            return ShopTypes;
        }

        public ShopTypes GetShopTypeByID(int ID) // აიდის მიხედვით მაღაზიის ტიპის გაგება
        {
            ShopTypes ShopTypes = new ShopTypes();
            ShopTypes = db.ShopTypes.FirstOrDefault(x => x.Id == ID);
            return ShopTypes;
        }


        public List<ShopTypes> GetShopTypes()
        {
            List<ShopTypes> shopType = new List<ShopTypes>();

            shopType = db.ShopTypes.ToList();
            return shopType;
        }

        public void ShopUpdate(int id, string name, string Address, ShopTypes ShopType)        
        {
            try
            {


                Shop Shop = new Shop();
                Shop = db.Shop.FirstOrDefault(x => x.Id == id);
                Shop.Name = name;
                Shop.Address = Address;
                if (ShopType != null)
                {
                    Shop.ShopType = ShopType;
                }
                
                db.Update(Shop);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void ProductRemove(int id)                 
        {
            try
            {


                db.Database.BeginTransaction();
                //int ShopProductsID = db.ShopProducts.FirstOrDefault(x => x.ProductId == id).Id;
                var shopproduct = db.ShopProducts.Where(x => x.ShopId == id);
                if (shopproduct != null)
                {
                    foreach (var item in shopproduct)
                    {
                        db.ShopProducts.Remove(db.ShopProducts.Find(item.Id));
                    }
                }
                db.Shop.Remove(db.Shop.Find(id));
                db.SaveChanges();
                db.Database.CommitTransaction();
            }
            catch (Exception ex)
            {

                db.Database.RollbackTransaction();
                throw new Exception(ex.Message);

            }
        }

        public Shop GetShopInfo(int ID)
        {
            Shop shop = new Shop();
            shop = db.Shop.FirstOrDefault(x => x.Id == ID);
            return shop;
        }

        public List<Shop> SelectShop()
        {
            List<Shop> shop = new List<Shop>();

            shop = db.Shop.ToList();
            return shop;
        }



        public List<Shop> searchShop(string name = null, string Address = null, ShopTypes ShopType = null)
        {
            List<Shop> Shop = new List<Shop>();
            if (name != null)
            {
                Shop = db.Shop.Where(x => x.Name.Contains(name)).ToList();
            }
            else
            if (Address != null)
            {
                Shop = db.Shop.Where(x => x.Address.Contains(Address)).ToList();
            }
            else
            if (ShopType != null)
            {
                Shop = db.Shop.Where(x => x.ShopType.ToString().Contains(ShopType.ToString())).ToList();
            }            
            return Shop;

        }

    }
}
