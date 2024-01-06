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

            List<CartItem> cartItems = string.IsNullOrEmpty(cookieValue)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(cookieValue);

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
        public class CheckOut
        {
            public Customer Customer { get; set; }
            public Order Order { get; set; }
            public List<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CheckOut checkout)
        {
            var model = new CheckOut
            {
                Customer = new Customer(),
                Order = new Order(),
                OrderDetail = new List<OrderDetail>(),
            };
            var check = _context.Customers.FirstOrDefault(m => m.UserName == checkout.Customer.UserName);
            if (check == null)
            {
                //thêm khách hàng
                _context.Customers.Add(checkout.Customer);
                _context.SaveChanges();
                //thêm hóa đơn
                int customeid = checkout.Customer.CustomerId;

                Order or = new Order
                {
                    CustomerId = customeid,
                    CreatedDate = DateTime.Now
                };
                _context.Add(or);
                _context.SaveChanges();
                //them sp vao hoa don chi tiet
                int orderid = or.OrderId;
                int i = 0;
                foreach (var item in checkout.OrderDetail)
                {
                    checkout.OrderDetail[i].OrderId = orderid;
                    _context.Add(checkout.OrderDetail[i]);
                    _context.SaveChanges();
                    i++;
                }
                TempData["AlertMessage"] = "Đặt thành công";
                Response.Cookies.Delete("cartItems");
            }
            else
            {
                //update
                TempData["AlertMessage"] = "trùng rồi";
            }

            return RedirectToAction("Index");
        }
    }
}
