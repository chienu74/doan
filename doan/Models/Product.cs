using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{
    [Table("tb_Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? Alias { get; set; }
        public int? CategoryProductId { get; set; }
        public int? DiscountsId { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public string? Image { get; set; }

        public double? Price { get; set; }
        public int? Quantity { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public bool IsNew { get; set; }
        public bool IsBestSeller { get; set; }
        public int? UnitInStock { get; set; }

        public bool? IsActive { get; set; }

    }
}
