using System;
using Xunit;
using ProductService.Api.Services;
using ProductService.Api.Model;
using ProductService.Api.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ProductService.Api.Tests
{
    public class ProductControllerTest
    {
        private IProductService _service;
        private ProductController _controller;
        public ProductControllerTest()
        {
            this._service = new ProductServiceFake();
            //or use this short equivalent 
            var logger = Mock.Of<ILogger<ProductController>>();
            _controller = new ProductController(logger, _service);

        }
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
    {
        // Act
        var okResult = _controller.Get();
 
        // Assert
        Assert.IsType<ActionResult<IEnumerable<Product>>>(okResult.Result);
    }

    }
}