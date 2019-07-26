using System;
using ProductService.Api.Infrastructure;
using System.Collections.Generic;
using ProductService.Api.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ProductService.Api.Infrastructure.Exceptions;

namespace ProductService.Api.Services
{

    public class ProductService : IProductService
    {
        private readonly ProductContext _context;
        public ProductService(ProductContext context)
        {
            this._context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<int> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }
        public async Task<int> UpdateProduct(Product product)
        {
            var item = await _context.Products.FirstOrDefaultAsync(i => i.Id == product.Id);
            item.ProductName = product.ProductName;
            await _context.SaveChangesAsync();
            return product.Id;

        }

        public async void DeleteProduct(Product item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}