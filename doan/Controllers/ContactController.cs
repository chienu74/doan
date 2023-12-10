using doan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace doan.Controllers
{
    public class ContactController : Controller
    {
        private readonly DataContext _context;
        public ContactController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var ctList = (from e in _context.Contacts
                          select new SelectListItem()
                          {
                              Text = e.Name,
                              Value = e.ContactId.ToString(),
                          }).ToList();
            ctList.Insert(0, new SelectListItem()
            {
                Text = "---Select---",
                Value = "0",
            });
            ViewBag.ctList = ctList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Index(Contact ct)
        {
            if(ModelState.IsValid)
            {
                _context.Contacts.Add(ct);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ct);
        }
    }
}
