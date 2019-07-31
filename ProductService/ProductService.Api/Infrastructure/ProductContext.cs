using Microsoft.EntityFrameworkCore;
using ProductService.Api.Model;
using System.Collections.Generic;
//using Product = ProductService.Api.Model;
//using ProductService.Api.Infrastructure;

namespace ProductService.Api.Infrastructure
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        { }

        public ProductContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=products.db");
        }

        public DbSet<Model.Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Product>().HasData(
                new Model.Product()
                {
                    Id = 1,
                    ProductName = "Product A"
                }
            );

            modelBuilder.Entity<Product>()
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        }       



    }
}