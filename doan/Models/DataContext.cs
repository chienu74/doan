using doan.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace doan.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Menu> Menus { get; set; }
        
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Blog_Category> Blog_Categories { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<View_Post_Menu> PostMenus { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }
<<<<<<< HEAD
        public DbSet<Account> Accounts { get; set; }
=======
        public DbSet<Contact> Contacts { get; set; }
>>>>>>> 83d9682a377a16f123a4e407ddb7433f925a74a4
    } 
}
