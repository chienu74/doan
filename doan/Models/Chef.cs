using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace doan.Models
{
    [Table("tb_Chef")]
    public class Chef
    {
        [Key]
        public int ChefID { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Position { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public int Status { get; set; }
    }
}

