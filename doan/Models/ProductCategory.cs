using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{
    [Table("tb_ProductCategory")]
    public class ProductCategory
    {
        [Key]
        public int CategoryProductId { get; set; }
        public string? Title { get; set; }
        public string? Alias { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public int? Position { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
