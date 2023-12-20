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
        public async Task<IViewComponentResult> InvokeAsync(/*Contact ct*/)
        {
/*            if (ModelState.IsValid)
            {
                _context.Contacts.Add(ct);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Tạo mới thành công";
            }
*/
            return await Task.FromResult((IViewComponentResult)View());
        }
    }
}
