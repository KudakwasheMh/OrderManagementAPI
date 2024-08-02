using System.ComponentModel.DataAnnotations;
namespace OrderManagementAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ClientId { get; set; }
        public Client Client{ get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public int OrderTotal { get;set; }
    }
}
