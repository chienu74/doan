﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using doan.Models;
using Microsoft.EntityFrameworkCore;
using doan.Areas.Admin.Models;
using X.PagedList;
using doan.Utilities;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductController : Controller
    {
        private readonly DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page) 
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;

            var prdList = _context.Products.OrderByDescending(m => m.ProductId).ToPagedList(pageNumber, pageSize);
            return View(prdList);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleProcduct = _context.Products.Find(id);
            if (deleProcduct == null)
            {
                return NotFound();
            }
            _context.Products.Remove(deleProcduct);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            var prdcatList = (from m in _context.ProductCategories
                              select new SelectListItem()
                              {
                                  Text = m.Title,
                                  Value = m.CategoryProductId.ToString()
                              }).ToList();
            prdcatList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn----",
                Value = "0"
            });
            ViewBag.prdcatList = prdcatList;
            var discoutList = (from m in _context.Discounts
                               select new SelectListItem()
                               {
                                   Text = "Giảm " + m.DiscountRate.ToString() + "%",
                                   Value = m.DiscountsId.ToString()
                               }).ToList();
            discoutList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn-----",
                Value = string.Empty
            });

            ViewBag.discoutList = discoutList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product prd)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(prd);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Thêm thành công";
                return RedirectToAction("Index");
            }
            return View(prd);
        }

        public IActionResult Edit(int? id)
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Products.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            var catList = (from m in _context.ProductCategories
                           select new SelectListItem()
                           {
                               Text = m.Title,
                               Value = m.CategoryProductId.ToString()
                           }).ToList();
            catList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn----",
                Value = string.Empty
            });
            var discoutList = (from m in _context.Discounts
                               select new SelectListItem()
                               {
                                   Text = "Giảm "+ m.DiscountRate.ToString() +"%",
                                   Value = m.DiscountsId.ToString()
                               }).ToList();
            discoutList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn-----",
                Value = string.Empty
            });

            ViewBag.catList = catList;
            ViewBag.discoutList = discoutList;
            return View(mn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product mn)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(mn);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Sửa thành công";
                return RedirectToAction("Index");
            }
            return View(mn);
        }
    }
}