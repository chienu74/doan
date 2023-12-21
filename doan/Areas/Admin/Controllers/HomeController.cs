﻿using Microsoft.AspNetCore.Mvc;
using doan.Utilities;
using doan.Models;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if(!Functions.IsLogin())
                return RedirectToAction("Index","Login");
            ViewBag.blogCount = _context.Blogs.Count();
            ViewBag.productCount = _context.Products.Count();
            return View();
        }
        public IActionResult Logout()
        {
            Functions._AccountID = 0;
            Functions._UserName= string.Empty;
            Functions._Email = string.Empty;
            Functions._Message = string.Empty;
            Functions._MessageEmail = string.Empty;
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
