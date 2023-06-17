using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPU123_Shop_MVC.Models;
using Data.Entities;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using DataAccess.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SPU123_Shop_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopDbContext context;

        private void LoadCategories()
        {
            ViewBag.CategoryList = new SelectList(context.Categories.ToList(), "Id", "Name");
        }

        public HomeController(ShopDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var products = context.Products.Include(x => x.Category).ToList();

            return View(products);
        }
        [HttpPost]
        public IActionResult Index(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }

            List<Product> products = context.Products.Include(x => x.Category).Where(p => p.Title.Contains(name)).ToList(); ;


            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult About()
        {
            LoadCategories();

            List<Product> products = context.Products.Include(x => x.Category).ToList(); ;


            return View(products);
        }
        [HttpPost]
        public IActionResult About(string category)
        {
            LoadCategories();
            if (category == null || category == "")
            {
                return NotFound();
            }

            List<Product> products = context.Products.Include(x => x.Category).Where(p => p.Category.Name.Contains(category)).ToList(); ;


            return View(products);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Details(int id)
        {
            var item = context.Products.Find(id);

            if (item == null)
                return NotFound();

            return View(item);
        }


    }
}
