using Microsoft.AspNetCore.Mvc;
using doan.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using doan.Utilities;
using X.PagedList;

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
        public IActionResult Index(int? page, string searchString)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            var pageNumber = page ?? 1;
            var pageSize = 10;
            IQueryable<doan.Models.Category> categories = _context.Categories.OrderByDescending(m => m.CategoryId);

            if (!string.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(m => m.Title.Contains(searchString));
            }

            IOrderedQueryable<doan.Models.Category> orderedMenus = categories.OrderByDescending(m => m.CategoryId);

            var pagedMenus = orderedMenus.ToPagedList(pageNumber, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.controllerName = "Menu";

            return View(pagedMenus);
        }
        //
        public IActionResult Delete(int? id)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
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
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
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
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
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
