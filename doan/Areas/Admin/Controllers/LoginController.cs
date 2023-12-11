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
			if (user == null)
			{
				return NotFound();
			}
			string pw = Functions.MD5Password(user.Password);
			var check = _context.Accounts.Where(m => (m.Email == user.Email) && (m.Password == pw)).FirstOrDefault();
			if (check == null)
			{
				Functions._Message = "Invalid UserName or Password!";
				return RedirectToAction("Index", "Login");
			}
			Functions._Message = string.Empty;
			Functions._AccountID = check.AccountId;
			Functions._UserName = string.IsNullOrEmpty(check.Username) ? string.Empty : check.Username;
			Functions._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
			Functions._Avatar = string.IsNullOrEmpty(check.Avatar) ? string.Empty : check.Avatar;

            return RedirectToAction("Index", "Home");
		}
	}
}