using ApiSale.BL;
using ApiSale.Models.ModelDTO;
using ApiSale.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSale.Controllers
{

    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    [ApiController]
    //אין אסינכרוניות!!!!!!!!!
    public class GiftController : Controller
    {
        private readonly IMapper mapper;
        private readonly IGiftService giftService;

        public GiftController(IMapper mapper, IGiftService giftService)
        {
            this.mapper = mapper;
            this.giftService = giftService;
        }
        // GET: DonorController/Create
        [AllowAnonymous]         
        [HttpGet]
        public async Task<List<GiftDTO>> GetAll()
        {
            return await giftService.GetGiftAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gift>> GetGiftByIdAsync(int id)
        {
            try
            {
                return await giftService.GetGiftByIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
        //get name donor
        [HttpGet("/nameDonor/{id}")]
        public async Task<ActionResult<string>> GetDonorByGift(int id)
        {
            try
            {
                return await giftService.GetDonorByGift(id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //חיפוש מתנות לפי שם מתנה
        [HttpGet("/searchGiftBy{name}")]
        public async Task<ActionResult<List<GiftDTO>>> SearchGiftByName(string name)
        {
            try
            {
                return await giftService.SearchGiftByNameGift(name);
            }
            //להחזיר שגיאת התחברות לשרת וכך בכל הקונטרולרים
            catch (Exception ex)
            {
                
                
                return NotFound(ex.Message);
            }
        }


        //חיפוש שם תורם לפי שם  תורם
        [HttpGet("/searchGiftByDonor/{donorName}")]
        public async Task<ActionResult<List<GiftDTO>>> SearchGiftByDonorName(string donorName)
        {
            try
            {
                return await giftService.SearchGiftByDonorName(donorName);
            }
            //להחזיר שגיאת התחברות לשרת וכך בכל הקונטרולרים
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("/ruffle/{giftId}")]
        public async Task<ActionResult<User>> RuffleGift(int giftId)
        {
            User user=new User();
            try

            {

                user= await giftService.RandomGift(giftId);
                if (user == null)
                    return NotFound("no one choose this gift!");
                return Ok(user);
            }
            //להחזיר שגיאת התחברות לשרת וכך בכל הקונטרולרים
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        // POST: DonorController/Create
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GiftDTO giftDTO)
        {
        try { 
            await giftService.AddGiftAsync(giftDTO);
            return Ok(new { message = "the gift added!!!!!!!" });
        }
            catch(DuplicateWaitObjectException ex) {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return Unauthorized(new { message = "You must be an Admin to access this data." });
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<Gift> Put(int id, [FromBody] GiftDTO giftDTO)
        {
          return  await giftService.UpdateGift(id, giftDTO);
        }

        // DELETE api/<CustomerController>/5
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await giftService.RemoveGift(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return  NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
