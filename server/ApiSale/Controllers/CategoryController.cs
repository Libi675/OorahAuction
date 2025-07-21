using ApiSale.BL;
using ApiSale.DAL;
using ApiSale.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        // POST api/<ProducrController>
        
        [HttpPost]
        public void Post([FromBody] Categorya categorya)
        {
            categoryService.AddAsync(categorya);
        }

       

        // DELETE api/<ProducrController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            categoryService.RemoveAsync(id);
        }
        // get api/<ProducrController>/5
        [HttpGet]
        public async Task<List<Categorya>> Get()
        {
            return await categoryService.GetCategoryas();
        }

    }
}
