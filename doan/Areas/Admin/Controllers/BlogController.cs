using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using doan.Models;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class BlogController : Controller
    {
        private readonly DataContext _context;
        public BlogController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var mnList = _context.Blogs.OrderBy(m => m.BlogId).ToList();
            return View(mnList);
        }
        //
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Blogs.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        [HttpPost]

        public IActionResult Delete(int id)
        {
            var deleMenu = _context.Blogs.Find(id);
            if (deleMenu == null)
            {
                return NotFound();
            }
            _context.Blogs.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            var catList = (from m in _context.Categories
                          select new SelectListItem()
                          {
                              Text = m.Title,
                              Value = m.CategoryId.ToString()
                          }).ToList();
            catList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });
            ViewBag.catList = catList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog mn)
        {
            if (ModelState.IsValid)
            {
                _context.Blogs.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Blogs.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            var catList = (from m in _context.Blogs
                          select new SelectListItem()
                          {
                              Text = m.Title,
                              Value = m.BlogId.ToString()
                          }).ToList();
            catList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.catList = catList;
            return View(mn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog mn)
        {
            if (ModelState.IsValid)
            {
                _context.Blogs.Update(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }
    }
}