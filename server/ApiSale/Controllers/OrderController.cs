using ApiSale.BL;
using ApiSale.Models;
using ApiSale.Models.ModelDTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//public class GiftController : Controller
//{
//    private readonly IMapper mapper;
//    private readonly IGiftService giftService;

//    public GiftController(IMapper mapper, IGiftService giftService)
//    {
//        this.mapper = mapper;
//        this.giftService = giftService;
//    }



namespace ApiSale.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IMapper mapper;
            private readonly IOrderSevice orderSevice;

           public OrderController(IMapper mapper, IOrderSevice orderSevice)
        {
            this.mapper = mapper;
            this.orderSevice = orderSevice;
        }
        // POST api/<ProducrController>

        [HttpPost("/addToCart/{Id}")]
        public async Task<ActionResult<string>> Post(int Id)
        {
            // categoryService.AddAsync(categorya);
            var objOrderDTO= new OrderDTO();
            try
            {
                var userIdClaim = User?.Claims.FirstOrDefault(c => c.Type == "UserId");
                if (userIdClaim == null)
                {
                    return Unauthorized("UserId claim is missing. Ensure the user is authenticated and the claim is present.");
                }
                objOrderDTO = new OrderDTO()
                {
                    GiftId = Id,
                    UserId = int.Parse(userIdClaim.Value),
                    IsDraft = false
                };
                await orderSevice.AddOrderAsync(objOrderDTO);
                return Ok(new { message = "the order added!!!!!!!" });
            } 
            catch (Exception ex)
            {
                return ex.Message;
            }

            //if (userIdClaim == null || !AuthorizeService(id, int.Parse(userIdClaim.Value)))
            //{
            //    return "User is not authorized to access this resource."; // סטטוס 403
            //}
            
        }

        [HttpPut("/toBuy")]
        public async Task<ActionResult<string>> Put()
        {
           
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
                var userId = int.Parse(userIdClaim.Value);
        
                await orderSevice.ToBuy(userId);
               return Ok(new { message = "the order !!!!!!!" });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
        [HttpGet("/getUserOrder")]
        public async Task<ActionResult<List<Order>>> Get()

        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
                var userId = int.Parse(userIdClaim.Value);
               
               return  await orderSevice.GetUserCart(userId);
                 

            }
            catch (Exception ex)
            {
               return NotFound(ex.Message);
            }




        }



        // DELETE api/<ProducrController>/5
        [HttpDelete("{orderId}")]
        public async Task<ActionResult<string>> Delete(int orderId)
        {
            try
            {
                await orderSevice.DeleteOrderAsync(orderId);
                return Ok(new { message = "the order delete!!!!!!!" });
              
            }
            catch (Exception ex) {
               return ex.Message;
            }
        }
        [HttpGet("/getPopular")]
        public async Task<ActionResult<List<Gift>>> PopularGift()
        {
            try
            {
               return   await orderSevice.PopularGift();
               
            }
            catch(Exception ex)
            {
                return StatusCode(500,"problem in server😱");
            }

        }
        [HttpGet("/GerUserOrders")]
        public async Task<ActionResult<List<User>>> GerUserOrders()
        {
            try
            {
                return await orderSevice.GerUserOrders();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "problem in server😱");
            }

        }
        [HttpGet("/GetOrderByGift/{giftId}")]
        public async Task<ActionResult<List<User>>> GetOrderByGift(int giftId)
        {
            try
            {
                return await orderSevice.GetOrderByGift(giftId);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "problem in server😱");
            }

        }

        [HttpGet("/GetProfit")]
        public async Task<ActionResult<int>> GetProfit()
        {
            try
            {
                return await orderSevice.Profit();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "problem in server😱");
            }

        }

    }
}
