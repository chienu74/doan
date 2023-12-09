using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using doan.Models;
using X.PagedList;
using Microsoft.CodeAnalysis.Scripting;
using System.Security.Cryptography;
using System.Xml.Linq;
using doan.Models.ho;

namespace doan.Controllers
{
    public class BlogController : Controller
    {
        private readonly DataContext _context;

        public BlogController(DataContext context)
        {
            _context = context;
        }

        // GET: Blogs
        public IActionResult Index(int? page = 1)
        {
            int pageSize = 6;
            var bl = _context.Blogs.Where(m => (m.IsActive) == true).OrderByDescending(n => n.BlogId).ToPagedList((int)page, pageSize);
            ViewBag.blogcomment = _context.BlogComments.ToList();
            return View(bl);
        }

        // GET: Blogs/Details/5
        [Route("/blog-{slug}-{id:long}.html", Name = "Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewBag.blogcomment = _context.BlogComments.Where(m => m.BlogId == id).ToList();
            ViewBag.category = _context.Categories.ToList();
            return View(blog);
        }

        [HttpPost]
        public IActionResult Create(int? id, string name, string phone, string email, string message)
        {
            try
            {
                TbBlogComment comment = new TbBlogComment();
                comment.BlogId = id;
                comment.Name = name;
                comment.Phone = phone;
                comment.Email = email;
                comment.Detail = message;
                comment.CreatedDate = DateTime.Now;
                _context.Add(comment);
                _context.SaveChangesAsync();
                return Json(new { status = true });
            }
            catch
            {
                return Json(new { status = false });
            }
        }
    }
}