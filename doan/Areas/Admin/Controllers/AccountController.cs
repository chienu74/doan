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

                // Cập nhật trường "Avatar" của đối tượng "Account"
                var account = _context.Accounts.Find(Functions._AccountID); // Đây chỉ là ví dụ, bạn cần tìm đúng đối tượng "Account" tương ứng với người dùng đang tải lên hình ảnh
                account.Avatar = "/files/avatar/" + fileName; // Lưu đường dẫn tương đối của hình ảnh

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
    }
}
