using System.ComponentModel.DataAnnotations;
namespace OrderManagementAPI.Models
{
    public class Vendor : User 
    {
        
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string VDescrip { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
