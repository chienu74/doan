using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Areas.Admin.Models
{
    [Table("tb_Account")]
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool? IsActive { get; set; }
    }
}
