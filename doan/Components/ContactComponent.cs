using Microsoft.AspNetCore.Mvc;
using doan.Models;

namespace doan.Components
{
    [ViewComponent(Name = "Contact")]
    public class ContactComponent : ViewComponent
    {
        private readonly DataContext _context;
        public ContactComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View());
        }
    }
}
