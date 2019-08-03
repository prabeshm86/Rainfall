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
using ProductService.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProductService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;

        public ProductController(ILogger<ProductController> logger, IProductService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            _logger.LogInformation("Get Products");
            return Ok(await _service.GetProducts());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _service.GetProduct(id);
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

            var id = await _service.AddProduct(product);

            return CreatedAtAction(nameof(AddProduct), new { Id = id });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody]Product product)
        {
            var item = await _service.GetProduct(product.Id);
            if (item == null)
            {
                throw new ProductDomainException("Product not found");
            }
            else
            {
                var id = await _service.UpdateProduct(product);
                return CreatedAtAction(nameof(UpdateProduct), new { id = id });
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProduct(int id)
        {
           // throw new Exception("Naughty!");
            var item = _service.GetProduct(id).Result;
            if (item == null)
            {
                throw new ProductDomainException("Product not found");
            }
            else
            {
                _service.DeleteProduct(item);
                return NoContent();
            }
        }
    }
}