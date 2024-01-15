using doan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using doan.Utilities;
using elFinder.NetCore;
using doan.Areas.Admin.Models;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Upload(IFormFile upimg)
        {
            if (upimg != null)
            {
                string filePath = "wwwroot/files/avatar";
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(upimg.FileName);
                string uploadedFilePath = Path.Combine(filePath, fileName);

                using (var fileStream = new FileStream(uploadedFilePath, FileMode.Create))
                {
                    upimg.CopyTo(fileStream);
                }
                var account = _context.Accounts.Find(Functions._AccountID);

                account.Avatar = "/files/avatar/" + fileName;
                _context.Update(account);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            int id = Functions._AccountID;
            var acc = _context.Accounts.Find(id);
            if (acc == null)
            {
                return NotFound();
            }
            return View(acc);
        }

        [HttpPost]
        public IActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Update(account);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }
        [HttpPost]
        public IActionResult EditPassword(int id, string newpassword)
        {
            if (ModelState.IsValid)
            {
                string newPW = Functions.MD5Password(newpassword);
                //update
                var account = _context.Accounts.FirstOrDefault(m => m.AccountId == id);
                if (account != null)
                {
                    account.Password = newPW;
                    _context.SaveChanges();
                    return RedirectToAction("Logout","Home");
                }
            }
			TempData["AlertMessage2"] = "Không đổi mật khẩu thì đùng có mà nhấp lung tung";
			return RedirectToAction("Index");

        }

    }
}
