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
        public static int pagenumber { get; set; }
        
       
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") == "admin") { 
                return View(); 
            }
            else
            {
                return RedirectToAction("login", "authorization");
            }
        }
       
        public IActionResult shopPage(int ID=1)
        {

            if (HttpContext.Session.GetString("username") == "admin")
            {
                ShopProcedures pro = new ShopProcedures();
                var shop = pro.GetShopsPage(pro.SelectShop(), ID);
                return View(shop);
            }
            else
            {
                return RedirectToAction("login", "authorization");
            }

        }

        public IActionResult ShopAdd()
        {
            if (HttpContext.Session.GetString("username") == "admin")
            {
                ShopProcedures pro = new ShopProcedures();
                var shop = pro.GetShopTypes();
                return View(shop);
            }
            else
            {
                return RedirectToAction("login", "authorization");
            }
        }

        [HttpPost]
        public IActionResult shopPage(string ID, string name, string Address, string Type, string name1, string searchComponent)
        {
            try
            {
                if (HttpContext.Session.GetString("username") == "admin")
                {
                    if (name1 != null)
                    {
                        ViewBag.name1 = name1;
                        if (searchComponent == "name")
                        {
                            ShopProcedures shop = new ShopProcedures();
                            var shop_result = shop.searchShop(name1, null);

                            ViewBag.searchComponent = "name";


                            return View(shop_result);
                        }
                        else if (searchComponent == "type")
                        {
                            ShopProcedures shop = new ShopProcedures();
                            var shop_result = shop.searchShop(null, null, name1);
                            ViewBag.searchComponent = "type";
                            return View(shop_result);
                        }
                        else if (searchComponent == "Address")
                        {
                            ShopProcedures shop = new ShopProcedures();
                            var shop_result = shop.searchShop(null, name1);
                            ViewBag.searchComponent = "Address";
                            return View(shop_result);
                        }

                    }


                    if (Convert.ToInt32(ID) != 0)
                    {
                        ShopProcedures shop = new ShopProcedures();
                        shop.ShopRemove(Convert.ToInt32(ID));

                        ShopProcedures selectshop = new ShopProcedures();
                        var shop_result = selectshop.GetShopsPage(selectshop.SelectShop(), 1);
                        return View(shop_result);
                    }
                    else
                    {
                        //string image_url = uploadfile(formFile);
                        ShopProcedures Shop = new ShopProcedures();
                        //product.ProductAdd(name, company, image_url);
                        var shoptype = Shop.GetShopType(Type);
                        Shop.ShopAdd(name, Address, shoptype.Id);

                        ShopProcedures pro = new ShopProcedures();
                        int lastpage = (pro.SelectShop().Count() / 5) + 1;
                        var shop = pro.GetShopsPage(pro.SelectShop(), lastpage);
                        return View(shop);
                    }
                }
                else
                {
                    return RedirectToAction("login", "authorization");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("/Home/Error");
                return View();
            }
        }

        [HttpGet]
        public IActionResult shopEdit(int ID)
        {
            if (HttpContext.Session.GetString("username") == "admin")
            {
                ShopProcedures shop = new ShopProcedures();
                var result = shop.GetShopInfo(ID);
                return View(result);
            }
            else
            {
                return RedirectToAction("login", "authorization");
            }
        }

        [HttpPost]
        public IActionResult shopEdit(int ID, string name, string address, string Type)
        {
            try
            {
                if (HttpContext.Session.GetString("username") == "admin")
                {

                    ShopProcedures shop = new ShopProcedures();
                    shop.ShopUpdate(ID, name, address, Type);

                    return RedirectToAction("shoppage", "Shop");
                }
                else
                {
                    return RedirectToAction("login", "authorization");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }


    }
}