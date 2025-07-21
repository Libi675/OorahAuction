using ApiSale.Models;

namespace ApiSale.BL
{
    public interface ICategoryService
    {
        void AddAsync(Categorya categorya);
        void RemoveAsync(int id);
        public Task<List<Categorya>> GetCategoryas();


    }
}
