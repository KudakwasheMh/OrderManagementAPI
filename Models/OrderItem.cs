using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OrderManagementAPI.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProdId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}
