using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdController : Controller
    {
        private readonly IRep _repository;

        public ProdController(IRep repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult> ProductLists()
        {
            try
            {

                var results = await _repository.GetAllProductsAsync();

                dynamic products = results.Select(p => new
                {
                    p.ProdId,
                    p.PName,
                    p.PDesc,
                    p.Image,
                    p.Price,
                    p.QuantityIS,
                    p.Vendor.VendorName
                });

                return Ok(products);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetProduct/{productId}")]
        public async Task<ActionResult> ProductById(int productId)
        {
            var product = await _repository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }


    }
}
