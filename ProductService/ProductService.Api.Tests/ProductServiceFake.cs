using System;
using System.Numerics;
using ProductService.Api.Services;
using ProductService.Api.Model;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProductService.Api.Tests
{
    
    public class ProductServiceFake : IProductService
    {
        List<Product> products = new List<Product>(){
        new Product(){ Id =1, ProductName = "Product 1" },
        new Product(){ Id =2, ProductName = "Product 2" },
        new Product(){ Id =3, ProductName = "Product 3" },
        new Product(){ Id =4, ProductName = "Product 4" },
        };  


        public Task<List<Product>> GetProducts(){
            return Task.Run(()=>  products) ;
        }

        public Task<Product> GetProduct(int id){
            return Task.Run(()=>products.FirstOrDefault(i =>i.Id == id));
        }
        public Task<int> AddProduct(Product product){
            product.Id = new Random().Next();
            products.Add(product);

            return Task.Run(()=> product.Id);
        }
        public Task<int> UpdateProduct(Product product){
            throw new NotImplementedException();
        }
        public void DeleteProduct(Product item){
            products.Remove(item);
        }
    }
}