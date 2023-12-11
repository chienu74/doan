using doan.Models;
<<<<<<< HEAD
using doan.Models.ho;
=======
>>>>>>> 83d9682a377a16f123a4e407ddb7433f925a74a4
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
            return View();
        }

        [HttpPost]
<<<<<<< HEAD
        public IActionResult Create(Contact ct)
        {
            if (ModelState.IsValid)
=======
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact ct)
        {
            if(ModelState.IsValid)
>>>>>>> 83d9682a377a16f123a4e407ddb7433f925a74a4
            {
                _context.Contacts.Add(ct);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
<<<<<<< HEAD
            return View(ct);
        }

       
    }
}
=======
            return View();
        }
    }
}
>>>>>>> 83d9682a377a16f123a4e407ddb7433f925a74a4
