using Microsoft.AspNetCore.Mvc;
using doan.Models;

namespace doan.Components
{
    [ViewComponent(Name ="RecentPost")]
    public class RecentPostComponent : ViewComponent
    {
        private readonly DataContext _context;
        public RecentPostComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofPost =(from p in _context.Blogs
                             where(p.IsActive==true) 
                             orderby p.BlogId descending
                             select p).Take(4).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofPost));
        }
    }
}
