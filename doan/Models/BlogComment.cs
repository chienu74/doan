using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{
    [Table("tb_BlogComment")]
    public class BlogComment
    {
        [Key]
        public int CommentId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Detail { get; set; }
        public int? BlogId { get; set; }
        public bool IsActive { get; set; }

       /* public virtual Blog? Blog { get; set; }*/
    }
}
