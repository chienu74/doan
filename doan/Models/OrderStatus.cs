using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{
    [Table("tb_OrderStatus")]
    public partial class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
