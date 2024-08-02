using Microsoft.EntityFrameworkCore;
using System;

namespace OrderManagementAPI.Models
{
    public class Repository: IRep
    {
        private readonly ODbContext _oDbContext;

        public Repository(ODbContext oDbContext)
        {
            _oDbContext = oDbContext;
        }

        //add product, orderitem, etc.
        public void Add<T>(T entity) where T : class
        {
            _oDbContext.Add(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _oDbContext.SaveChangesAsync() > 0;
        }

        //Get all products 
        public async Task<Product[]> GetAllProductsAsync()
        {
            IQueryable<Product> query = _oDbContext.Products.Include(p => p.Vendor);
            return await query.ToArrayAsync();
        }

        //Get specific products by id
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _oDbContext.Products
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(p => p.ProdId == productId);
        }

        //Delete product
        public async Task<bool> ProdDelete(int productId)
        {
            var prodToDelete = await _oDbContext.Products.FindAsync(productId);

            if (prodToDelete != null)
            {
                _oDbContext.Products.Remove(prodToDelete);
                await _oDbContext.SaveChangesAsync();
                return true; 
            }

            return false;  
        }

        //Get OrderItems
        public async Task<OrderItem[]> GetAllOrderItemsAsync()
        {
            IQueryable<OrderItem> query = _oDbContext.OrderItems.Include(oi => oi.Order).Include(oi => oi.Product);
            return await query.ToArrayAsync();
        }

        //Get Order Items in an Order
        public async Task<List<OrderItem>> GetOrderOrderItemsAsync(int orderId)
        {
            return await _oDbContext.OrderItems
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
        }

        //Get Orders
        public async Task<Order[]> GetAllOrdersAsync()
        {
            IQueryable<Order> query = _oDbContext.Orders.Include(o => o.Client).Include(o => o.OrderItems);
            return await query.ToArrayAsync();
        }

        //Get CLient Orders
        public async Task<List<Order>> GetClientOrdersAsync(string clientId)
        {
            return await _oDbContext.Orders
                .Include(o => o.Client)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.ClientId == clientId)
                .ToListAsync();
        }

        //Get Client Order
        public async Task<Order> GetClientOrderAsync(int orderId, string clientId)
        {
            return await _oDbContext.Orders                
                .Include(o => o.Client).Where(o => o.ClientId == clientId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
        
        //Delete Order
        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _oDbContext.Orders
               .Include(o => o.OrderItems) // Include related OrderItems
               .ThenInclude(oi => oi.Product) // Include related Products in OrderItems
               .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order != null) { 
                foreach (var orderItem in order.OrderItems)
                {
                    var prod = orderItem.Product;
                    if (prod != null)
                    {
                        prod.QuantityIS += orderItem.Quantity;
                    }
                }
                _oDbContext.Orders.Remove(order);
                _oDbContext.SaveChanges();
            }
        }

        public async Task<Client>GetClientByIDAsync(string clientId)
        {
            return await _oDbContext.Clients
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
        }

        public async Task<Vendor> GetVendorByIDAsync(string vendorId)
        {
            return await _oDbContext.Vendors
                .FirstOrDefaultAsync(c => c.VendorId == vendorId);
        }



    }
}
