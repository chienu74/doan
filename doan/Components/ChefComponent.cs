﻿using Microsoft.AspNetCore.Mvc;
using doan.Models;
namespace doan.Components
{
    [ViewComponent(Name = "Chef")]
    public class ChefComponent : ViewComponent
    {
        private readonly DataContext _context;
        public ChefComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listChef = (from c in _context.Chefs
                            where (c.IsActive == true) && (c.Status == 1)
                            orderby c.ChefID descending
                            select c).Take(3).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listChef
                ));
        }
    }
}

