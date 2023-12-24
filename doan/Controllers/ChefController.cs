using doan.Models;
using Microsoft.AspNetCore.Mvc;


namespace doan.Controllers
{
    public class ChefController : Controller
    {
        private readonly DataContext _context;
        public ChefController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
