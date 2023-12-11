using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using doan.Models;
using Microsoft.EntityFrameworkCore;
using doan.Areas.Admin.Models;
using X.PagedList;

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
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var blList = _context.Blogs.OrderBy(m => m.BlogId).ToPagedList(pageNumber,pageSize);
            return View(blList);
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
        public IActionResult Create(Blog bl)
        {
            if (ModelState.IsValid)
            {
                _context.Blogs.Add(bl);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bl);
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
            var catList = (from m in _context.Categories
                           select new SelectListItem()
                           {
                               Text = m.Title,
                               Value = m.CategoryId.ToString()
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