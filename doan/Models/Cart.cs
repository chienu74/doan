using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace doan.Models
{
    public class Cart
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public double? Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }

    }
}
