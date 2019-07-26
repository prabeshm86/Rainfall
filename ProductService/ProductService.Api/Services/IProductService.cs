using System.Collections.Generic;
using ProductService.Api.Model;
using System.Threading.Tasks;

namespace ProductService.Api.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();

        Task<Product> GetProduct(int id);
        Task<int> AddProduct(Product product);
        Task<int> UpdateProduct(Product product);
        void DeleteProduct(Product item);
    }
}