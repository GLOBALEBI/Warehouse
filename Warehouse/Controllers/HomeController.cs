using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            ProductProcedures pro = new ProductProcedures();
            var product = pro.SelectProducts();
            return View(product);
        }

        [HttpPost]
        public IActionResult Privacy(string name, string company, string image_url)
        {
            ProductProcedures product = new ProductProcedures();
            product.ProductAdd(name, company, image_url);

            ProductProcedures pro = new ProductProcedures();
            var product_result = pro.SelectProducts();
            return View(product_result);
            
        }

        [HttpPost]
        public IActionResult Privacy(string ID)
        {
            ProductProcedures product = new ProductProcedures();
            product.ProductRemove(Convert.ToInt32(ID));

            ProductProcedures pro = new ProductProcedures();
            var product_result = pro.SelectProducts();
            return View(product_result);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
