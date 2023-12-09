using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace doan.Areas.Admin.Models
{
    [Table("tb_Blog_Category")]
    public class Blog_Category
    {
        [Key]
        public int BCId { get; set; }
        public int BlogId { get; set; }
        public int CategoryId { get; set; }
    }
}
