using X.PagedList;

using doan.Models;
using Microsoft.AspNetCore.Mvc;
using doan.Utilities;

namespace doan.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class NotificationController : Controller
    {
        private readonly DataContext _context;
        public NotificationController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            var pageNumber = page ?? 1;
            var pageSize = 10;
            var notif= _context.Contacts.OrderByDescending(m => m.CreatedDate).ToPagedList(pageNumber,pageSize);
            return View(notif);
        }
    }
}
