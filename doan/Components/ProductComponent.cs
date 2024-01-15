using doan.Models;
using Microsoft.AspNetCore.Mvc;
using static doan.Controllers.ProductController;

namespace doan.Components
{
    [ViewComponent(Name = "Product")]
    public class ProductComponent : ViewComponent
    {
        private DataContext _context;
        public ProductComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.ProductCategory = _context.ProductCategories.Where(m => m.IsActive == true).ToList();
            var prd = from product in _context.Products
                      join discount in _context.Discounts on product.DiscountsId equals discount.DiscountsId
                      select new doan.Controllers.ProductController.Product_Discount
                      {
                          Name = product.Title,
                          Price = product.Price - product.Price * (discount.DiscountRate / 100),
                          Img = product.Image,
                          CatId = product.CategoryProductId,
                          PrdId = product.ProductId,
                          Detail = product.Detail,
                          Cost = product.Price,
                      };
            return await Task.FromResult<IViewComponentResult>(View(prd.ToList()));
        }
    }
}
