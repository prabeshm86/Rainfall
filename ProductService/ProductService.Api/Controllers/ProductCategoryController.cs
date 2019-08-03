using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductService.Api.Infrastructure;
using ProductService.Api.Model;
using ProductService.Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductCategoryController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductContext _context;

        public ProductCategoryController(ILogger<ProductController> logger, ProductContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok();
            //return Ok(await _context.ProductCategories.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            return Ok();
           // return Ok(await _context.ProductCategories.FirstOrDefaultAsync(i => i.Id == id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddCategory([FromBody]ProductCategory productCategory)
        {
            var category = new ProductCategory
            {
                Name = productCategory.Name,
                Description = productCategory.Description,
            };

           // _context.ProductCategories.Add(category);
           // _context.SaveChanges();

            return CreatedAtAction(nameof(AddCategory), new { id = category.Id });
        }
    }
}
