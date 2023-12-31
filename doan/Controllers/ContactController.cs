﻿using doan.Models;
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact ct)
        {
            if(ModelState.IsValid)
            {
                _context.Contacts.Add(ct);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Thêm thành công";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
