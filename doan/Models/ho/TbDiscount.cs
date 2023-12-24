/*using System;
using System.Collections.Generic;

namespace doan.Models.ho
{
    public partial class TbDiscount
    {
        public TbDiscount()
        {
            TbOrderDetails = new HashSet<TbOrderDetail>();
        }

        public int DiscountsId { get; set; }
        public int? ProductId { get; set; }
        public int? DiscountRate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Product? Product { get; set; }
        public virtual ICollection<TbOrderDetail> TbOrderDetails { get; set; }
    }
}
*/