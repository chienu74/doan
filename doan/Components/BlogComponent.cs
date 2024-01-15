using Microsoft.AspNetCore.Mvc;
using doan.Models;

namespace doan.Components
{
    [ViewComponent(Name = "Blog")]
    public class BlogComponent : ViewComponent
    {
        private readonly DataContext _context;
        public BlogComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofBlog = (from p in _context.Blogs
                              where (p.IsActive == true)
                              orderby p.BlogId descending
                              select p).Take(2).ToList();
            ViewBag.comment = _context.BlogComments.ToList();
            ViewBag.cat = _context.Categories.ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofBlog));
        }
    }
}
