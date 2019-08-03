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

        public DbSet<Product> Products { get; set; }
//public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Model.Product()
                {
                    Id = 1,
                    ProductName = "Iphone X",
                    CategoryId = 1
                }
            );

            // modelBuilder.Entity<ProductCategory>().HasData(
            //     new ProductCategory { Id = 1, Name = "Mobile Phone" },
            //     new ProductCategory { Id = 2, Name = "Tablets" },
            //     new ProductCategory { Id = 3, Name = "Laptops" }
            // );

            modelBuilder.Entity<Product>()
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        }



    }
}