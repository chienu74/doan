using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using doan.Models;
using X.PagedList;
using doan.Utilities;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ChefController : Controller
    {
        private readonly DataContext _context;
        public ChefController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var blList = _context.Chefs.OrderByDescending(m => m.ChefID).ToPagedList(pageNumber, pageSize);
            return View(blList);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleChef = _context.Chefs.Find(id);
            if (deleChef == null)
            {
                return NotFound();
            }
            _context.Chefs.Remove(deleChef);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Chef chef)
        {
            if (ModelState.IsValid)
            {
                _context.Chefs.Add(chef);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Thêm thành công";
                return RedirectToAction("Index");
            }
            return View(chef);
        }

        public IActionResult Edit(int? id)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var chef = _context.Chefs.Find(id);
            if (chef == null)
            {
                return NotFound();
            }
            return View(chef);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Chef chef)
        {
            if (ModelState.IsValid)
            {
                _context.Chefs.Update(chef);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Sửa thành công";
                return RedirectToAction("Index");
            }
            return View(chef);
        }
    }
}