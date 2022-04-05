using System.Collections.Generic;

namespace OrderApi.Models
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string From { get; set; }
        public string SendAddress { get; set; }
        public decimal TotalMoney { get; set; }

        public List<OrderProductDto> Products { get; set; }
    }
}
