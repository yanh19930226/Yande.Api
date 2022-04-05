using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductApi.Controllers
{
    [Route("productapi/[controller]")]
    public class ProductController : ControllerBase
    {
        private List<ProductDto> productDtos = new List<ProductDto>();
        public ProductController()
        {
            productDtos.Add(new ProductDto { Id = 1, Name = "酒精", Price = 22.5m });
            productDtos.Add(new ProductDto { Id = 2, Name = "84消毒液", Price = 19.9m });
            productDtos.Add(new ProductDto { Id = 3, Name = "医用口罩", Price = 55 });
        }

        [HttpGet("get/{id}")]
        public ProductDto Get(long id)
        {
            return productDtos.FirstOrDefault(i => i.Id == id);
        }

        [HttpGet("getall")]
        public IEnumerable<ProductDto> GetAll()
        {
            return productDtos;
        }
    }
}
