using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Vendor")]
    public class VendorController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRep _repository;
        private readonly IConfiguration _config;
        private readonly ODbContext _context;

        public VendorController(UserManager<User> userManager, IRep repository, IConfiguration configuration, ODbContext context)
        {
            _repository = repository;
            _userManager = userManager;
            _config = configuration;
            _context = context;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] IFormCollection formData)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();

                var file = formCollection.Files.First();

                if (file.Length > 0)
                {

                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string base64 = Convert.ToBase64String(fileBytes);

                        var price = int.Parse(formData["price"]);
                        var quantiy = int.Parse(formData["quantity"]);

                        var product = new Product
                        {
                            
                            Price = price
                            ,
                            PName = formData["name"]
                            ,
                            PDesc = formData["description"]
                            ,
                            Image = base64
                            ,
                            QuantityIS = quantiy
                            ,
                            VendorId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value // Assuming VendorId is the same as user's ID
                        };


                        _repository.Add(product);
                        await _repository.SaveChangesAsync();
                    }

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        [Route("EditProduct/{productId}")]
        public async Task<ActionResult<Product>> EditCourse(int productId, Product prod)
        {
            try
            {
                var existingProd = await _repository.GetProductByIdAsync(productId);
                if (existingProd == null)
                {
                    return NotFound($"The course with ID {productId} does not exist.");
                }
                existingProd.PName = prod.PName;
                existingProd.PDesc = prod.PDesc;
                existingProd.Image = prod.Image;
                existingProd.Price = prod.Price;
                existingProd.QuantityIS = prod.QuantityIS;

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingProd); // Return the updated course
                }
                else
                {
                    return StatusCode(500, "Failed to update the course. Please try again.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpDelete]
        [Route("DeleteProduct/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var vendorId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (vendorId == null)
            {
                return Unauthorized();
            }

            var result = await _repository.ProdDelete(productId);
            if (result)
            {
                return Ok(new { message = "Product deleted successfully" });
            }
            else
            {
                return NotFound(new { message = "Product not found or not owned by the vendor" });
            }
        }

        [HttpGet]
        [Route("GetVendorDetails")]
        [Authorize]
        public async Task<IActionResult> GetUserDetails()
        {
            // Retrieve the vendor ID from the claims
            var vendorId = User.Claims.FirstOrDefault(c => c.Type == "VendorId")?.Value;
            if (vendorId == null)
            {
                return Unauthorized(new { message = "Vendor ID not found in claims." });
            }

            var vendor = await _repository.GetVendorByIDAsync(vendorId);
            if (vendor == null)
            {
                return NotFound(new { message = "Vendor not found" });
            }

            // Return the vendor details
            return Ok(new
            {
                UserName = vendor.UserName,
                Email = vendor.Email,
                VendorName = vendor.VendorName
            });
        }

    }
}
