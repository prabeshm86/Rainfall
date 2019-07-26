using System;
namespace ProductService.Api.Infrastructure.Exceptions
{
    public class ProductDomainException : Exception
    {
        public ProductDomainException(string message) : base(message)
        {
            
        }
    }
}