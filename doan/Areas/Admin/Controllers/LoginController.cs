using doan.Areas.Admin.Models;
using doan.Models;
using doan.Utilities;
using Microsoft.AspNetCore.Mvc;


namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly DataContext _context;
        public LoginController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Account user)
        {
            if (user.Email==null || user.Password == null)
            {
                Functions._Message = "Mời nhập mật khẩu và email!";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                string pw = Functions.MD5Password(user.Password);
                var check = _context.Accounts.Where(m => (m.Email == user.Email) && (m.Password == pw)).FirstOrDefault();
                if (check == null)
                {
                    Functions._Message = "Sai mật khẩu hoặc email!";
                    return RedirectToAction("Index", "Login");
                }
                Functions._Message = string.Empty;
                Functions._AccountID = check.AccountId;
                Functions._UserName = string.IsNullOrEmpty(check.Username) ? string.Empty : check.Username;
                Functions._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
                Functions._Avatar = string.IsNullOrEmpty(check.Avatar) ? string.Empty : check.Avatar;
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId == Functions._AccountID);
                if (account != null)
                {
                    account.LastLogin = DateTime.Parse(Functions.getCurrentDate());
                    _context.Update(account);
                    _context.SaveChanges();
                }
            return RedirectToAction("Index", "Home");
            }
        }
    }
}