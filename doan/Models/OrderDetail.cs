using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{
    [Table("tb_OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

    }
}
