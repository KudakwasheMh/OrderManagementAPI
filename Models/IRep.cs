using Microsoft.EntityFrameworkCore;

namespace OrderManagementAPI.Models
{
    public interface IRep
    {
        Task<bool> SaveChangesAsync();

        void Add<C>(C entity) where C : class;

        Task<Product[]> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int productId);
        Task<bool> ProdDelete(int productId);
        Task<OrderItem[]> GetAllOrderItemsAsync();

        Task<List<OrderItem>> GetOrderOrderItemsAsync(int orderI);

        Task<Order[]> GetAllOrdersAsync();

        Task<List<Order>> GetClientOrdersAsync(string clientId);

        Task<Order> GetClientOrderAsync(int orderId, string clientId);

        Task DeleteOrderAsync(int orderId);

        Task<Client> GetClientByIDAsync(string clientId);
        Task<Vendor> GetVendorByIDAsync(string vendorId);
    }
}
