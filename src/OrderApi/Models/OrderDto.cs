namespace OrderApi.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string From { get; set; }
        public string SendAddress { get; set; }
        public decimal TotalMoney { get; set; }
    }
}
