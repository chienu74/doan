using Microsoft.AspNetCore.Mvc;
using doan.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using doan.Utilities;
using X.PagedList;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly DataContext _context;
        public ProductCategoryController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page, string searchString)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            var pageNumber = page ?? 1;
            var pageSize = 10;
            IQueryable<doan.Models.ProductCategory> categories = _context.ProductCategories.OrderByDescending(m => m.CategoryProductId);

            if (!string.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(m => m.Title.Contains(searchString));
            }

            IOrderedQueryable<doan.Models.ProductCategory> orderedMenus = categories.OrderByDescending(m => m.CategoryProductId);

            var pagedMenus = orderedMenus.ToPagedList(pageNumber, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.controllerName = "p";

            return View(pagedMenus);
        }
        //

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var deleCat = _context.ProductCategories.Find(id);
            if (deleCat == null)
            {
                return NotFound();
            }
            _context.ProductCategories.Remove(deleCat);
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
        public IActionResult Create(ProductCategory prcat)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            if (ModelState.IsValid)
            {
                _context.ProductCategories.Add(prcat);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Thêm thành công";
                return RedirectToAction("Index");
            }
            return View(prcat);
        }

        public IActionResult Edit(int? id)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var cat = _context.ProductCategories.Find(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductCategory cat)
        {
            if (ModelState.IsValid)
            {
                _context.ProductCategories.Update(cat);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Sửa thành công";
                return RedirectToAction("Index");
            }
            return View(cat);
        }
    }
}
