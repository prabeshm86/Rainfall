using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Api.Model
{
    public class Product
    {
        public Product()
        {
            //this.Id = -1;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        //public ProductCategory Category { get; set; }
        public string ImageUrl { get; set; }
    }
}