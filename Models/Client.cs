using System.ComponentModel.DataAnnotations;
namespace OrderManagementAPI.Models
{
    public class Client : User
    {
        
        public string ClientId { get; set; }

        public string ClientName { get; set; }

        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
