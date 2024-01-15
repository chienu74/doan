using doan.Models;
using Microsoft.AspNetCore.Mvc;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly DataContext _context;
        public OrderController(DataContext context)
        {
            _context = context;
        }
        public class OrderList
        {
            public string UserName { get; set; }
            public string? OrderStatusName { get; set; }
            public int OrderId { get; set; }
            public double? TotalAmount { get; set; }
            public DateTime? OrderDate { get; set; }
        }
        public class OrderDetailList
        {
            public string? ProductName { get; set; }
            public int? Quantity { get; set; }
            public double? Price { get; set; }
            public double? TotalAmount  { get; set; }
        }
        public IActionResult Index()
        {
            var or = from ord in _context.Orders
                     join cus in _context.Customers
                     on ord.CustomerId equals cus.CustomerId
                     join ors in _context.OrderStatuses
                     on ord.OrderStatusId equals ors.OrderStatusId
                     orderby ord.OrderId descending
                     select new OrderList
                     {
                         OrderId = ord.OrderId,
                         OrderStatusName = ors.Name,
                         UserName = cus.UserName,
                         OrderDate= ord.CreatedDate,
                         TotalAmount=ord.TotalAmount,
                     };
            return View(or.ToList());
        }
        public IActionResult OrderDetail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var ord = from ordt in _context.OrderDetails
                      join pro in _context.Products on ordt.ProductId equals pro.ProductId
                      where ordt.OrderId == id
                      select new OrderDetailList
                      {
                          ProductName=pro.Title,
                          Quantity = ordt.Quantity,
                          Price = ordt.Price,
                          TotalAmount = ordt.Price * (double)ordt.Quantity,
                      };

            if (ord == null)
            {
                return NotFound();
            }
            return View(ord.ToList());
        }
    }
}
