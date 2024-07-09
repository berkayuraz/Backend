using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "products.json");

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [EnableCors]
        [HttpGet(Name = "products")]
        public async Task<IActionResult> GetProducts()
        {
            if (!System.IO.File.Exists(_filePath))
            {
                return NotFound("File not found.");
            }

            var json = await System.IO.File.ReadAllTextAsync(_filePath);
            var products = JsonConvert.DeserializeObject<List<Product>>(json);
            return Ok(products);
        }
    }
}
