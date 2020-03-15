using Microsoft.AspNetCore.Mvc;
using WooliesXChallenge.Services.Interfaces;

namespace WooliesXChallenge.Controllers
{
    //
    // Summary:
    //      ProductController, this controller is for exercise2
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET api/product/sort
        [HttpGet("api/product/sort")]
        public IActionResult Get(string sortOption)
        {
            return Ok(_productService.GetAll(sortOption));
        }
    }
}
