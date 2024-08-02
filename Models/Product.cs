using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace OrderManagementAPI.Models
{
    public class Product
    {
        [Key]
        public int ProdId { get; set; }
        public string? Image { get; set; }
        public string PName { get; set; }
        public string PDesc { get; set; }
        public int Price { get; set; }
        public int QuantityIS { get; set; }

        public string VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }


    }
}
