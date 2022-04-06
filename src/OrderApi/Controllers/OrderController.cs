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
        private readonly Nacos.V2.INacosNamingService _serverManager;

        public OrderController(IHttpClientFactory clientFactory, Nacos.V2.INacosNamingService serverManager)
        {
            orderDtos.Add(new OrderDto { Id = 1, TotalMoney = 222, Address = "北京市",  From = "淘宝", SendAddress = "武汉" });
            orderDtos.Add(new OrderDto { Id = 2, TotalMoney = 111, Address = "北京市", From = "京东", SendAddress = "北京" });
            orderDtos.Add(new OrderDto { Id = 3, TotalMoney = 333, Address = "北京市",  From = "天猫", SendAddress = "杭州" });

            _clientFactory = clientFactory;
            _serverManager= serverManager;
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

                //内部调用ProductApi,配合自定义的NacosDiscoveryDelegatingHandler可以更优雅的使用注册中心方式
                //var client = _clientFactory.CreateClient(ServiceName.ProductService);
                //var response = await client.GetAsync($"/productapi/product/getall");
                //var result = await response.Content.ReadAsStringAsync();

                var instance = await _serverManager.SelectOneHealthyInstance("productservice", "DEFAULT_GROUP");
                var host = $"{instance.Ip}:{instance.Port}";

                var baseUrl = instance.Metadata.TryGetValue("secure", out _)
                    ? $"https://{host}"
                    : $"http://{host}";
                var url = $"{baseUrl}/productapi/product/getall";

                using (HttpClient client = new HttpClient())
                {
                    var result = await client.GetAsync(url);
                     var res=await result.Content.ReadAsStringAsync();
                    orderDetailDto.Products = JsonConvert.DeserializeObject<List<OrderProductDto>>(res);
                }
            }
            return orderDto;
        }
    }
}
