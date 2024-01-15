using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{
    [Table("tb_Discounts")]
    public class Discount
    {
        [Key]
        public int DiscountsId { get; set; }
        public double? DiscountRate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
