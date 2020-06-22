using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Warehouse.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult shopPage()
        {
            ShopProcedures pro = new ShopProcedures();
            var shop = pro.SelectShop();
            return View(shop);
        }

        public IActionResult ShopAdd()
        {
            ShopProcedures pro = new ShopProcedures();
            var shop = pro.GetShopTypes();
            return View(shop);
        }

        [HttpPost]
        public IActionResult shopPage(string ID, string name, string Address, string Type, string name1, string searchComponent)
        {
            //try
            //{

                if (name1 != null)
                {
                    if (searchComponent == "name")
                    {
                        ShopProcedures shop = new ShopProcedures();
                        var shop_result = shop.searchShop(name1, null);
                        return View(shop_result);
                    }
                    else
                    {
                        ShopProcedures shop = new ShopProcedures();
                        var shop_result = shop.searchShop(null, name1);
                        return View(shop_result);
                    }

                }


                if (Convert.ToInt32(ID) != 0)
                {
                    ShopProcedures shop = new ShopProcedures();
                    shop.ProductRemove(Convert.ToInt32(ID));

                    ProductProcedures pro = new ProductProcedures();
                    var product_result = pro.SelectProducts();
                    return View(product_result);
                }
                else
                {
                    //string image_url = uploadfile(formFile);
                    ShopProcedures Shop = new ShopProcedures();
                    //product.ProductAdd(name, company, image_url);
                    var shoptype = Shop.GetShopType(Type);
                    Shop.ShopAdd(name, Address, shoptype.Id);

                    ShopProcedures pro = new ShopProcedures();
                    var shop = pro.SelectShop();
                    return View(shop);
                }
            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("/Home/Error");
            //    return View();
            //}
        }

        public IActionResult productDelete()
        {
            return View();
        }


    }
}