using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderApi.Controllers
{
    [Route("orderapi/[controller]")]
    public class OrderController : ControllerBase
    {
        private List<OrderDto> orderDtos = new List<OrderDto>();
        private readonly IHttpClientFactory _clientFactory;

        public OrderController(IHttpClientFactory clientFactory)
        {
            orderDtos.Add(new OrderDto { Id = 1, TotalMoney = 222, Address = "北京市",  From = "淘宝", SendAddress = "武汉" });
            orderDtos.Add(new OrderDto { Id = 2, TotalMoney = 111, Address = "北京市", From = "京东", SendAddress = "北京" });
            orderDtos.Add(new OrderDto { Id = 3, TotalMoney = 333, Address = "北京市",  From = "天猫", SendAddress = "杭州" });

            _clientFactory = clientFactory;
        }

        [HttpGet("get/{id}")]
        public OrderDto GetOrder(long id)
        {
            return orderDtos.FirstOrDefault(i => i.Id == id);
        }

        [HttpGet("getdetails/{id}")]
        public async Task<OrderDto> GetOrderDetailsAsync(long id)
        {
            OrderDto orderDto = GetOrder(id);

            if (orderDto != null)
            {
                OrderDetailDto orderDetailDto = new OrderDetailDto
                {
                    Id = orderDto.Id,
                    TotalMoney = orderDto.TotalMoney,
                    Address = orderDto.Address,
                    From = orderDto.From,
                    SendAddress = orderDto.SendAddress
                };
                var client = _clientFactory.CreateClient(ServiceName.ProductService);
                var response = await client.GetAsync($"/productapi/product/getall");
                var result = await response.Content.ReadAsStringAsync();
                orderDetailDto.Products = JsonConvert.DeserializeObject<List<OrderProductDto>>(result);
            }
            return orderDto;
        }

        [HttpGet("getcart")]
        public async Task<string> GetCart()
        {
                var client = _clientFactory.CreateClient(ServiceName.CartService);
                var response = await client.GetAsync($"/cartapi/getcart");
                var result = await response.Content.ReadAsStringAsync();
               return result;
        }
    }
}
