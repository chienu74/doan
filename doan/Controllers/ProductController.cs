using doan.Models;
using Microsoft.AspNetCore.Mvc;

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
            var prd = _context.Products.Where(m => m.IsActive == true).OrderBy(i => i.ProductId).ToList();
            return View(prd);
        }
    }
}
