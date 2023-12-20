using Microsoft.AspNetCore.Mvc;
using doan.Models;
using doan.Utilities;
using System.Globalization;

namespace doan.Areas.Admin.Componets
{
    [ViewComponent(Name = "Notification")]
    public class NotificationComponent : ViewComponent
    {
        private readonly DataContext _dataContext;
        public NotificationComponent(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.nCount = _dataContext.Contacts.Count(i => i.CreatedDate.HasValue
            && i.CreatedDate.Value > DateTime.ParseExact(Functions.getTime(), "dd-MM-yyyy", CultureInfo.InvariantCulture));

            ViewBag.acccca = _dataContext.Accounts.Where(i => i.AccountId == Functions._AccountID).FirstOrDefault();

            ViewBag.accList = _dataContext.Accounts.Where(m => m.AccountId != Functions._AccountID).OrderByDescending(i => i.LastLogin).Take(4).ToList();

            var mnList = _dataContext.Contacts.ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", mnList));
        }
    }
}
