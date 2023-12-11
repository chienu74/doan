using Microsoft.AspNetCore.Mvc;
using doan.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly DataContext _context;
        public CategoryController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var mnList = _context.Categories.OrderBy(m => m.CategoryId).ToList();
            return View(mnList);
        }
        //
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var cat = _context.Categories.Find(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }
        [HttpPost]

        public IActionResult Delete(int id)
        {
            var deleCat = _context.Categories.Find(id);
            if (deleCat == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(deleCat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            var mnList = (from m in _context.Menus
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuId.ToString()
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });
            ViewBag.mnList = mnList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(cat);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var cat = _context.Categories.Find(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category cat)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(cat);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat);
        }
    }
}
