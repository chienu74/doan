using Microsoft.EntityFrameworkCore;

namespace doan.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        { 
        }
        public DbSet<Menu> Menus {  get; set; }
    }
}
