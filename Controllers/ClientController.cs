using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Models;
using OrderManagementAPI.Models.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;

namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRep _repository;
        private readonly IConfiguration _config;
        private readonly ODbContext _context;

        public ClientController(UserManager<User> userManager, IRep repository, IConfiguration configuration, ODbContext context)
        {
            _repository = repository;
            _userManager = userManager;
            _config = configuration;
            _context = context;
        }

        //Get client past orders
        [HttpGet]
        [Route("GetClientOrders/{clientId}")]
        public async Task<ActionResult> ClientOrderListing(string clientId)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (UserId == null)
            {
                return Unauthorized();
            }

            var orders = await _repository.GetClientOrdersAsync(clientId);
            if (orders == null)
            {
                return NotFound("You do not have any past orders");
            }
            return Ok(orders);
        }

        //Get client past order 
        [HttpGet]
        [Route("GetClientOrder/{orderId}/{clientId}")]
        public async Task<ActionResult> ClientOrder(int orderId, string clientId)
        {

            var UserId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (UserId == null)
            {
                return Unauthorized();
            }

            var order = await _repository.GetClientOrderAsync(orderId, clientId);
            if (order == null)
            {
                return NotFound("This order does not exist anymore");
            }
            return Ok(order);
        }

        //Place an order
        [HttpPost]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(OrderRequest or)
        {
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (clientId == null)
            {
                return Unauthorized();
            }

            // Fetch the products from the database to ensure valid quantities and prices
            var productIds = or.OrderItems.Select(oi => oi.ProdId).ToList();
            var products = _context.Products.Where(p => productIds.Contains(p.ProdId)).ToDictionary(p => p.ProdId);

            if (products.Count != productIds.Count)
            {
                return BadRequest("One or more products are invalid.");
            }

            int orderTotal = 0;

            var orderItems = new List<OrderItem>();
            foreach (var oi in or.OrderItems)
            {
                if (!products.TryGetValue(oi.ProdId, out var product))
                {
                    return BadRequest($"Product with ID {oi.ProdId} not found.");
                }

                if (product.QuantityIS < oi.Quantity)
                {
                    return BadRequest($"Insufficient quantity for product {product.PName}.");
                }

                int itemTotal = product.Price * oi.Quantity;
                orderTotal += itemTotal;

                var orderItem = new OrderItem
                {
                    OrderItemID = oi.OrderItemID,
                    ProdId = oi.ProdId,
                    Product = oi.Product,
                    Quantity = oi.Quantity,
                    Total = itemTotal
                };

                product.QuantityIS -= oi.Quantity;
                orderItems.Add(orderItem);
            }
                var order = new Order
                {
                    ClientId = clientId,
                    OrderDate = DateTime.Now,
                    OrderTotal = orderTotal,
                    OrderItems = orderItems
                };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        //Details of client that is logged in
        [Authorize]
        [HttpGet]
        [Route("GetClientDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            var claims = User.Claims.ToList();
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }
            // Retrieve the client ID from the claims
            var clientId = claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (clientId == null)
            {
                return Unauthorized(new { message = "Client ID not found in claims." });
            }

            var client = await _repository.GetClientByIDAsync(clientId);
            if (client == null)
            {
                return NotFound(new { message = "Client not found" });
            }

            // Return the client details
            return Ok(new
            {
                UserName = client.UserName,
                Email = client.Email,
                ClientName = client.ClientName,
                LastName = client.LastName
            });
        }

        //Get client order from past orders
        [HttpGet]
        [Route("GetClientOrder")]
        public async Task<IActionResult> GetClientOrder(int orderId)
        {
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (clientId == null)
            {
                return Unauthorized();
            }

            var order = await _repository.GetClientOrderAsync(orderId, clientId);
            if (order == null)
            {
                return NotFound(new { message = "Order not found" });
            }

            return Ok(order);
        }

        [HttpGet]
        [Route("GetOrderItems/{orderId}")]
        public async Task<IActionResult> GetOrderItems(int orderId)
        {
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (clientId == null)
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            // Assuming you have a method to check if the order belongs to the logged-in user
            var order = await _repository.GetClientOrderAsync(orderId, clientId);
            if (order == null)
            {
                return NotFound(new { message = "Order not found or you do not have access to this order." });
            }

            var orderItems = await _repository.GetOrderOrderItemsAsync(orderId);

            if (orderItems == null || !orderItems.Any())
            {
                return NotFound(new { message = "No order items found for this order." });
            }

            return Ok(orderItems);
        }

        [HttpDelete]
        [Route("DeleteOrder/{orderId}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                // Call the repository method to delete the order and update product quantities
                await _repository.DeleteOrderAsync(orderId);

                return Ok(new { message = "Order deleted successfully." });
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
