using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Models;
using Microsoft.AspNetCore.Hosting;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNetCore.Http;

namespace Warehouse.Controllers
{
    public class productController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public productController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") == "admin")
            {
                ProductProcedures pro = new ProductProcedures();
                var product = pro.SelectProducts();
                return View(product);
            }
            else
            {
                return RedirectToAction("login", "authorization");
            }
        }

        [HttpPost]
        public IActionResult Index(string ID, string name, string company, IFormFile formFile, string name1, string searchComponent)
        {
            try
            {
                if (HttpContext.Session.GetString("username") == "admin")
                {
                    if (name1 != null)
                    {
                        if (searchComponent == "name")
                        {
                            ProductProcedures pro = new ProductProcedures();
                            var product_result = pro.searchProducts(name1, null);
                            return View(product_result);
                        }
                        else
                        {
                            ProductProcedures pro = new ProductProcedures();
                            var product_result = pro.searchProducts(null, name1);
                            return View(product_result);
                        }

                    }


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
                        string image_url = uploadfile(formFile);
                        ProductProcedures product = new ProductProcedures();
                        product.ProductAdd(name, company, image_url);

                        ProductProcedures pro = new ProductProcedures();
                        var product_result = pro.SelectProducts();
                        return View(product_result);
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
        public IActionResult productEdit(int ID)
        {
            if (HttpContext.Session.GetString("username") == "admin")
            {
                ProductProcedures product = new ProductProcedures();
                var result = product.GetProductInfo(ID);
                return View(result);
            }
            else
            {
                return RedirectToAction("login", "authorization");
            }
        }

        [HttpPost]
        public IActionResult productEdit(int ID, string name, string company, IFormFile formFile)
        {          
            try
            {
                if (HttpContext.Session.GetString("username") == "admin")
                {
                    string image_url = null;
                    if (formFile != null)
                    {
                        image_url = uploadfile(formFile);
                    }

                    ProductProcedures product = new ProductProcedures();
                    product.ProductUpdate(ID, name, company, image_url);
                    //Response.Redirect("/product/Index");
                    //return View("~/View/product/Index.aspx");
                    return RedirectToAction("Index", "product");
                }
                else
                {
                    return RedirectToAction("login", "authorization");
                }
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
            if (HttpContext.Session.GetString("username") == "admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("login", "authorization");
            }
        }


        //ფაილები
       
        private string FileDirectoryName()        //აგენერირებს ფაილის დირექტორიის სახელს
        {
            return $"{DateTime.Now.Year}/{DateTime.Now.Month}/";
        }

        private void checkAndCreateDirectory(string path)        //ამოწმებს არსებობს თუარა დირექტორია თუ არარსებობს ქმნის
        {
            bool exists = Directory.Exists(Path.Combine(_hostEnvironment.WebRootPath, path));
            if (!exists)
            {
                Directory.CreateDirectory(Path.Combine(_hostEnvironment.WebRootPath, path));
            }
        }
       
        private string fileVersoinCheckAndUpdate(string filename, string path, string ext)  
        {
            int count = 1;
            string newfilename = filename;
            string newpath = Path.Combine(path, filename + ext);

            while(System.IO.File.Exists(Path.Combine(_hostEnvironment.WebRootPath, newpath)))
            {
                newfilename = string.Format("{0}({1})", filename, count++);
                newpath = Path.Combine(path, newfilename + ext);
            }
            return newfilename;
        }

        private string uploadfile(IFormFile file)
        {
            if (file.Length > 0)
            {
                string name = Path.GetFileNameWithoutExtension(file.FileName);
                string ext = Path.GetExtension(file.FileName);
                string filedirectoryname = FileDirectoryName();
                checkAndCreateDirectory($"Upload/{filedirectoryname}");
                name = fileVersoinCheckAndUpdate(name, $"Upload/{filedirectoryname}", ext);
                var path = Path.Combine(_hostEnvironment.WebRootPath, "Upload", filedirectoryname + name + ext);

                using (var stream = System.IO.File.Create(path))
                {
                    file.CopyTo(stream);
                }
                return Path.Combine("Upload", filedirectoryname + name + ext);
            }
            return string.Empty;
           
        }
    }
}
