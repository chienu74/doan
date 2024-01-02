using doan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace doan.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ProductCategory = _context.ProductCategories.Where(m => m.IsActive == true).ToList();
            var prd = _context.Products.Where(m => m.IsActive == true).OrderBy(i => i.ProductId).ToList();
            return View(prd);
        }
    }
}
