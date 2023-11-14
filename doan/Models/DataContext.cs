using Microsoft.EntityFrameworkCore;

namespace doan.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<View_Post_Menu> PostMenus { get; set;}
    }
}
