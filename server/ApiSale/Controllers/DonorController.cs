using ApiSale.BL;
using ApiSale.Models;
using ApiSale.Models.ModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSale.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : Controller
    {
        private readonly IDonorService donorService;

        public DonorController(IDonorService donorService)
        {
            this.donorService = donorService;
        }

        // GET: DonorController/Create
        [AllowAnonymous]
        [HttpGet]
        public async Task<List<Donor>> GetAll()
        {
            return await donorService.GetAsync();
        }

        // GETById: DonorController/Create
        [HttpGet("{id}")]
        public async Task<Donor> GetById(int id)
        {
            return await donorService.GetById(id);
        }

        // POST: DonorController/Create
        //[Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task Post([FromBody] DonorDTO donorDTO)
        {     
              await  donorService.AddDonorAsync(donorDTO);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] DonorDTO donorDTO)
        {
          await donorService.UpdateDonorAsync(id, donorDTO);
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
             await donorService.RemoveAsync(id);
             return Ok();
            }
            catch(KeyNotFoundException ex)

            {
                return NotFound(ex.Message);
            }

        }
        // לתורם רשימת מתנות
        [HttpGet("/list Gift{id}")]
        public async Task<List<Gift>> Get(int id) => await donorService.GetGiftsByDonor(id);
        //
        [HttpGet("/search donor{parm}")]
        public async Task<List<Donor>> Get(string  parm) => await donorService.SearchDonor(parm);
    }
}
