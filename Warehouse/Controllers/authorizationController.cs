﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Warehouse.Models;


namespace Warehouse.Controllers
{
    public class authorizationController : Controller
    {
        WarehouseContext db = new WarehouseContext();
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(string username, string password)
        {

            var user = db.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if(user!=null && user.Id>0)
            {             
                HttpContext.Session.SetString("username", "admin");
                ViewBag.login_text = "ავტორიზაცია წარმატებით დასრულდა";
                //Response.Redirect("");
                return RedirectToAction("Index", "product");

            }
            else
            {
                ViewBag.login_text = "username or password incorect!";
                return View();

            }
            
            
        }
    }
}