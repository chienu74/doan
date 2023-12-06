using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace doan.Models
{
    [Table("tb_Category")]
    public class Category
    {
       /* public Category()
        {
            Blogs = new HashSet<Blog>();
        }*/
        [Key]
        public int CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Alias { get; set; }
        public string? Description { get; set; }
        public int? Position { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

     /*   public virtual ICollection<Blog> TbBlogs { get; set; }
        public HashSet<Blog> Blogs { get; }*/
    }
}
