using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductService.Api.Infrastructure;
using ProductService.Api.Model;
using Microsoft.EntityFrameworkCore;
using ProductService.Api.Infrastructure.Exceptions;

namespace ProductService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductContext _productContext;

        public ProductController(ILogger<ProductController> logger, ProductContext context)
        {
            _logger = logger;
            _productContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            _logger.LogInformation("Get Products");
            return Ok(await _productContext.Products.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _productContext.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (product == null)
            {
                throw new ProductDomainException("Id not found");
            }
            else
            {
                return Ok(product);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody]Product product)
        {
            var item = new Product
            {
                ProductName = product.ProductName
            };

            _productContext.Products.Add(item);
            await _productContext.SaveChangesAsync();

            return CreatedAtAction(nameof(AddProduct), new { Id = item.Id });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody]Product product)
        {
            var item = await _productContext.Products.FirstOrDefaultAsync(i => i.Id == product.Id);
            if (item == null)
            {
                throw new ProductDomainException("Product Not found");
            }
            else
            {
                item.ProductName = product.ProductName;
                await _productContext.SaveChangesAsync();

                return CreatedAtAction(nameof(UpdateProduct), new { id = item.Id });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var item = await _productContext.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (item == null)
            {
                throw new ProductDomainException("Product Not found");
            }
            else
            {
                _productContext.Remove(item);
                await _productContext.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}