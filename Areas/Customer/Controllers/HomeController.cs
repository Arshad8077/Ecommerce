using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Areas.Admin.Controllers;
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Controllers
{

    [Area("Customer")]
    public class HomeController : Controller
    {

        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }


        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View(_db.Product.Include(c=>c.Category).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET Product detail action method

        public ActionResult Detail(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = _db.Product.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        //POST product detail action method

        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)
        {

            List<Product>products=new List<Product>();
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Product.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products = HttpContext.Session.Get<List<Product>>("products");
            if(products == null)
            {
                products = new List<Product>(); 
            }
            products.Add(product); 
            HttpContext.Session.Set("products", products);
            return View(product);
            //    return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }


        //GET product Cart action method

        public IActionResult Cart()
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }
            return View(products);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
