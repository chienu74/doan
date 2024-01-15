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
            string cookieValue = Request.Cookies["cartItems"]?.ToString();

            List<CartItem> cartItems = string.IsNullOrEmpty(cookieValue) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cookieValue);

            var cart = (from cartitem in cartItems
                        join product in _context.Products on cartitem.ProductId equals product.ProductId
                        join discount in _context.Discounts on product.DiscountsId equals discount.DiscountsId
                        select new Cart
                        {
                            ProductId = product.ProductId,
                            Title = product.Title,
                            Image = product.Image,
                            Price = product.Price - product.Price * (discount.DiscountRate / 100),
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
        public class Booking
        {
            public Customer Customer { get; set; }
            public Order Order { get; set; }
            public List<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            var check0 = _context.Customers.FirstOrDefault(m => m.UserName == booking.Customer.UserName && m.Email == booking.Customer.Email);
            var check1 = booking.OrderDetail.Count();

            if (check1 != 0)
            {
                int customeid = 0;
                if (check0 == null)
                {
                    _context.Customers.Add(booking.Customer);
                    _context.SaveChanges();
                    customeid = booking.Customer.CustomerId;
                }
                else
                {
                    string? name = booking.Customer.UserName;
                    string? email = booking.Customer.Email;
                    customeid = _context.Customers.Where(m => m.UserName == name && m.Email==email).First().CustomerId;
                }
 
                Order or = new Order
                {
                    CustomerId = customeid,
                    CreatedDate = DateTime.Now,
                    OrderStatusId = 3,
                };
                _context.Add(or);
                _context.SaveChanges();
 
                int orderid = or.OrderId;
                int i = 0;
                foreach (var item in booking.OrderDetail)
                {
                    booking.OrderDetail[i].OrderId = orderid;
                    _context.Add(booking.OrderDetail[i]);
                    _context.SaveChanges();
                    i++;
                }
                double? totalAmount = 0;
                foreach (var item in booking.OrderDetail)
                {
                    totalAmount += item.Price * (double)item.Quantity;
                }
                var ta = _context.Orders.Where(m=>m.OrderId == orderid).First();
                ta.TotalAmount = totalAmount;
                _context.SaveChanges();

                TempData["AlertMessage"] = "Đặt thành công";
                Response.Cookies.Delete("cartItems");
            }
            else
            {
                TempData["AlertMessage"] = "Đừng để giỏ hàng trống";
            }

            return RedirectToAction("Index");
        }
    }
}
