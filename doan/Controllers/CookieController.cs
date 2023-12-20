using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using doan.Models;
using System.Security.Cryptography.X509Certificates;

namespace doan.Controllers
{
    public class CookieController : Controller
    {
        private readonly DataContext _context;
        public CookieController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var pr = _context.Products.OrderBy(p => p.ProductId).ToList();
            return View(pr);
        }
        public IActionResult SetCookie()
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("MyCookie", "Hello hello", option);
            return RedirectToAction("Index");
        }
        public IActionResult ReadCookie()
        {
            string cookieValue = Request.Cookies["MyCookie"];
            ViewBag.CookieValue = cookieValue;
            return View();
        }
    }
}
