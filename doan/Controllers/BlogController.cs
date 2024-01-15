using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using doan.Models;
using X.PagedList;

namespace doan.Controllers
{
    public class BlogController : Controller
    {
        private readonly DataContext _context;

        public BlogController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page, string? searchString)
        {
            var pageNumber = page ?? 1;
            int pageSize = 6;
            IQueryable<Blog> blogs = _context.Blogs.OrderByDescending(m=>m.BlogId);
            if(!string.IsNullOrEmpty(searchString) )
            {
                blogs = blogs.Where(m => m.CategoryId.ToString().Contains(searchString));
            }
            IOrderedQueryable<Blog> or = blogs.OrderByDescending(m => m.BlogId);

            var paged = or.ToPagedList(pageNumber,pageSize);
            ViewBag.blogcomment = _context.BlogComments.ToList();
            ViewBag.category = _context.Categories.ToList();
            return View(paged);
        }

        public class Blog_BlogComment
        {
            public BlogComment BlogComment { get; set; }
            public Blog Blog { get; set; }
        }
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
            var blog_blogComment = new Blog_BlogComment
            {
                Blog = blog,
                BlogComment = new BlogComment()
            };
            ViewBag.blogcomment = _context.BlogComments.Where(m => m.BlogId == id).ToList();
            ViewBag.category = _context.Categories.ToList();
            ViewBag.blog = _context.Blogs.ToList();
            return View(blog_blogComment);
        }
        [HttpPost]
        public IActionResult Create(Blog_BlogComment blvm)
        {

            if (blvm != null)
            {
                _context.BlogComments.Add(blvm.BlogComment);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Bình luận thành công";
            }
            return Redirect("/blog-slug-" + blvm.BlogComment.BlogId + ".html");
        }
    }
}