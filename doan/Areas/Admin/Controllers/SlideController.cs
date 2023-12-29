using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using doan.Models;
using X.PagedList;
using doan.Utilities;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class SlideController : Controller
    {
        private readonly DataContext _context;
        public SlideController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;

            var slList = _context.Slides.OrderByDescending(m => m.SlideId).ToPagedList(pageNumber, pageSize);
            return View(slList);
        }
        //

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleSlide = _context.Slides.Find(id);
            if (deleSlide == null)
            {
                return NotFound();
            }
            _context.Slides.Remove(deleSlide);
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
        public IActionResult Create(Slide sl)
        {
            if (ModelState.IsValid)
            {
                _context.Slides.Add(sl);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Thêm thành công";
                return RedirectToAction("Index");
            }
            return View(sl);
        }

        public IActionResult Edit(int? id)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var sl = _context.Slides.Find(id);
            if (sl == null)
            {
                return NotFound();
            }
            return View(sl);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slide sl)
        {
            if (ModelState.IsValid)
            {
                _context.Slides.Update(sl);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Sửa thành công";
                return RedirectToAction("Index");
            }
            return View(sl);
        }
    }
}