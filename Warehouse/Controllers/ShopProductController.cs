using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Models;
using Microsoft.AspNetCore.Http;

namespace Warehouse.Controllers
{
    public class ShopProductController : Controller
    {
        
        WarehouseContext db = new WarehouseContext();
        public IActionResult Index()
        {
            //ShopProductProcedures shopproduct = new ShopProductProcedures();
            //var sho = shopproduct.getShopProducts();

            if (HttpContext.Session.GetString("username") == "admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("login", "authorization");
            }
        }
        [HttpPost]
        public IActionResult Index(string shop_id, string shop_id2, string productName=null, string barcode=null, string productPrice=null)
        {
            try
            {

                if (HttpContext.Session.GetString("username") == "admin")
                {

                    if (productName != null && barcode != null && productPrice != null)
                    {
                        ShopProductProcedures add = new ShopProductProcedures();
                        add.AddShopProduct(shop_id2, productName, barcode, Convert.ToDouble(productPrice));

                        ViewBag.selected = db.Shop.FirstOrDefault(x => x.Id == Convert.ToInt32(shop_id2)).Name;
                        //throw new Exception("shecdomaaaaaaaaaaaaaaa");
                        ShopProductProcedures shopproduct = new ShopProductProcedures();
                        var result = shopproduct.getShopProductsInfo(Convert.ToInt32(shop_id2));
                        return View(result);
                    }
                    else
                    {
                        ViewBag.selected = db.Shop.FirstOrDefault(x => x.Id == Convert.ToInt32(shop_id)).Name;
                        //throw new Exception("shecdomaaaaaaaaaaaaaaa");
                        ShopProductProcedures shopproduct = new ShopProductProcedures();
                        var result = shopproduct.getShopProductsInfo(Convert.ToInt32(shop_id));
                        return View(result);
                    }
                }

                else
                {
                    return RedirectToAction("login", "authorization");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrrorMessage = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
