using System;
using System.Collections.Generic;

namespace doan.Models.ho
{
    public partial class TbProductCategory
    {
        public TbProductCategory()
        {
            TbProducts = new HashSet<Product>();
        }

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

        public virtual ICollection<Product> TbProducts { get; set; }
    }
}
