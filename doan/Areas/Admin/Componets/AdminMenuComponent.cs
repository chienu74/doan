using Microsoft.AspNetCore.Mvc;
using doan.Models;

namespace doan.Areas.Admin.Componets
{
    [ViewComponent(Name ="AdminMenu")]
    public class AdminMenuComponent : ViewComponent
    {
        private readonly DataContext _dataContext;
        public AdminMenuComponent(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnList = (from mn in _dataContext.AdminMenus
                          where (mn.IsActive == true)
                          select mn).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", mnList));
        }
    }
}
