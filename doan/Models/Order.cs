using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{ 
    [Table("tb_Order")]
    public partial class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? OrderStatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
