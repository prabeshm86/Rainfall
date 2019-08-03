using System;
using System.Collections.Generic;

namespace ProductService.Api.Model
{
    public class ProductCategory
    {
        public ProductCategory()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
