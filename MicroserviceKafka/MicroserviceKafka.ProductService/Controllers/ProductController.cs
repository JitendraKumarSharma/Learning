using MicroserviceKafka.Model;
using MicroserviceKafka.ProductService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceKafka.ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ProductDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<List<ProductModel>> GetProducts()
        {
            return await dbContext.Products.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound();
            }

            return product;
        }
    }
}
