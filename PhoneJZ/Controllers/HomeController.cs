using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneJZ.App_Data;

namespace PhoneJZ.Controllers
{
    public class HomeController : Controller
    {
        private PhoneDB db = new PhoneDB();
        public ActionResult Index(string category)
        {
            ViewData["GetCategory"] = db.Categories.ToList();
            var product = from m in db.Products
                          select m;
            if (!String.IsNullOrEmpty(category))
            {
                product = product.Include(e=>e.Category1).Where(e=>e.Category1.name.Contains(category));
            }

           return View(product.ToList());
            
        }
        public ActionResult _CategoryPartial()
        {
            return PartialView("_CategoryPartial",db.Categories.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}