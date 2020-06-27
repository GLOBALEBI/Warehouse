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

        public void ShopUpdate(int id, string name, string Address, string ShopType)        
        {
            try
            {


                Shop Shop = new Shop();
                Shop = db.Shop.FirstOrDefault(x => x.Id == id);
                Shop.Name = name;
                Shop.Address = Address;
                var shoptype = GetShopType(ShopType);
                Shop.ShopTypeId = shoptype.Id;
                db.Update(Shop);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void ShopRemove(int id)                 
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



        public List<Shop> searchShop(string name = null, string Address = null, string ShopType = null)
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
                var shoptype = db.ShopTypes.FirstOrDefault(x => x.Type.Contains(ShopType));
                Shop = db.Shop.Where(x => x.ShopTypeId == shoptype.Id).ToList();

            }            
            return Shop;
        }


        public List<Shop> GetShopsPage(List<Shop> shops, int page)     //პარამეტრად იღებს მაღაზიების სიას და გვერდის ნომერს
        {                                                                  //და აბრუნებს შესაბამისი გვერძე გამოსაჩენ მაღაზიების სიას.
            int shopsize = shops.Count();
            
            List<Shop> ShopsPage = new List<Shop>();
            int i = (page - 1) * 5;      //დასაწყისი
            int j = (page * 5)-1;      //დასასრული
            while (i <= j)
            {
                if (i <= shops.Count()-1)
                {
                    ShopsPage.Add(shops[i]);
                    i++;
                }
                else
                {
                    break;
                }
            }

            return ShopsPage;
            
        }

    }
}
