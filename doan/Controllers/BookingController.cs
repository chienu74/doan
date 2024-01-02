using doan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Web;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace doan.Controllers
{
    public class BookingController : Controller
    {
        private readonly DataContext _context;

        public BookingController(DataContext context)
        {
            _context = context;
        }
        public class CartItem
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
        public IActionResult Index()
        {
            string cookieValue = Request.Cookies["cartItems"].ToString();

            List<CartItem> cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cookieValue);

            var cart = (from cartitem in cartItems
                        join product in _context.Products
                        on cartitem.ProductId equals product.ProductId
                        select new Cart
                        {
                            ProductId = product.ProductId,
                            Title = product.Title,
                            Image = product.Image,
                            Price = product.Price,
                            Quantity = cartitem.Quantity
                        }).ToList();
            ViewBag.CartItems = cart;
            int count = 0;
            foreach (var item in cart)
            {
                count += item.Quantity;
            }
            ViewBag.Count = count;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer cus)
        {
            var check = _context.Customers.Where(m => m.UserName == cus.UserName).ToList();
            if (check.Count() == 0)
            {
                _context.Customers.Add(cus);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Thêm thành công";
            }
            else
            {
                var existingCustomer = _context.Customers.FirstOrDefault(m => m.UserName == cus.UserName);
                if (existingCustomer != null)
                {
                    existingCustomer.UserName = cus.UserName;
                    // Cập nhật các thông tin khác tương ứng

                    _context.SaveChanges();
                    TempData["AlertMessage"] = "Cập nhật thành công";
                }
            }

            return RedirectToAction("Index");
        }
    }
}
