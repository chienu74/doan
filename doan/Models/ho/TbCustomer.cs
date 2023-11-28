using System;
using System.Collections.Generic;

namespace doan.Models.ho
{
    public partial class TbCustomer
    {
        public TbCustomer()
        {
            TbOrders = new HashSet<TbOrder>();
        }

        public int CustomerId { get; set; }
        public string? UserName { get; set; }
        public int? AccountId { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Avatar { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? LocationId { get; set; }
        public bool IsActive { get; set; }

        public virtual TbAccount? Account { get; set; }
        public virtual TbLocation? Location { get; set; }
        public virtual ICollection<TbOrder> TbOrders { get; set; }
    }
}
