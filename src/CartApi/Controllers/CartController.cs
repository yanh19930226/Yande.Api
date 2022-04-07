using Microsoft.AspNetCore.Mvc;

namespace CartApi.Controllers
{
    [Route("cartapi/[controller]")]
    public class CartController : ControllerBase
    {
        [HttpGet("getcart")]
        public string Get()
        {
            return "cart";
        }
    }
}
