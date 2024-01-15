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
        public class Product_Discount
        {
            public string? Name { get; set; }
            public string? Detail { get; set; }
            public double? Cost { get; set; }
            public double? Price { get; set; }
            public string? Img { get; set; }
            public int? CatId {  get; set; } 
            public int? PrdId {  get; set; }
            public double? Rate { get; set; }
        }
        public IActionResult Index()
        {
            ViewBag.ProductCategory = _context.ProductCategories.Where(m => m.IsActive == true).ToList();
            var prd = from product in _context.Products
                      join discount in _context.Discounts on product.DiscountsId equals discount.DiscountsId
                      select new Product_Discount
                      {
                          Name = product.Title,
                          Price = product.Price - product.Price * (discount.DiscountRate / 100),
                          Img = product.Image,
                          CatId = product.CategoryProductId,
                          PrdId = product.ProductId,
                          Detail = product.Detail,
                          Cost = product.Price,
                      };
            return View(prd.ToList());
        }
    }
}
