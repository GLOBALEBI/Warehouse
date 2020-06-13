using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    public class productController : Controller
    {
        public IActionResult Index()
        {
            ProductProcedures pro = new ProductProcedures();
            var product = pro.SelectProducts();
            return View(product);
        }

        [HttpPost]
        public IActionResult Index(string ID, string name, string company, string image_url)
        {
            try
            {

                if (Convert.ToInt32(ID) != 0)
                {
                    ProductProcedures product1 = new ProductProcedures();
                    product1.ProductRemove(Convert.ToInt32(ID));

                    ProductProcedures pro = new ProductProcedures();
                    var product_result = pro.SelectProducts();
                    return View(product_result);
                }
                else
                {
                    ProductProcedures product = new ProductProcedures();
                    product.ProductAdd(name, company, image_url);

                    ProductProcedures pro = new ProductProcedures();
                    var product_result = pro.SelectProducts();
                    return View(product_result);
                }
            }
            catch (Exception ex)
            {  
                Response.Redirect("/Home/Error");
                return View();
            }
        }

        [HttpGet]
        public IActionResult productEdit(int ID)
        {
            ProductProcedures product = new ProductProcedures();
            var result = product.GetProductInfo(ID);
            return View(result);
        }

        [HttpPost]
        public IActionResult productEdit(int ID, string name, string company, string image_url)
        {
            try
            {              
                ProductProcedures product = new ProductProcedures();
                product.ProductUpdate(ID, name, company, image_url);
                //Response.Redirect("/product/Index");
                //return View("~/View/product/Index.aspx");
                return RedirectToAction("Index", "product");
                
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

            public IActionResult productDelete()
        {     
            return View();
        }

        public IActionResult productAdd()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Index(string ID)
        //{
        //    ProductProcedures product = new ProductProcedures();
        //    product.ProductRemove(Convert.ToInt32(ID));

        //    ProductProcedures pro = new ProductProcedures();
        //    var product_result = pro.SelectProducts();
        //    return View(product_result);

        //}
    }
}
