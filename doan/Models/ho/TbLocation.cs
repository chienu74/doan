using System;
using System.Collections.Generic;

namespace doan.Models.ho
{
    public partial class TbLocation
    {
        public TbLocation()
        {
            TbCustomers = new HashSet<TbCustomer>();
        }

        public int LocationId { get; set; }
        public string? Name { get; set; }
        public int? Parent { get; set; }

        public virtual ICollection<TbCustomer> TbCustomers { get; set; }
    }
}
