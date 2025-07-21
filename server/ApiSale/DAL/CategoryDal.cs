using ApiSale.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSale.DAL
{
    public class CategoryDal : ICategotyDal
    {
        private readonly ChainaSaleDBContext chainaSaleDBContext;

        public CategoryDal(ChainaSaleDBContext chainaSaleDBContext)
        {
            this.chainaSaleDBContext = chainaSaleDBContext;
        }

        public async void AddAsync(Categorya categorya)
        {
            var newCategory = categorya;
            await chainaSaleDBContext.Categorya.AddAsync(newCategory);
            //async??????????????????????
            chainaSaleDBContext.SaveChanges();
        }

       

        public async void RemoveAsync(int id)
        {
           var categoryRemove=chainaSaleDBContext.Categorya.FirstOrDefault(c=>c.CategoryaId==id);
           
          
        }
        public async Task<List<Categorya>> GetCategoryas()
        {
           return await chainaSaleDBContext.Categorya.ToListAsync();
        }
    }
}
