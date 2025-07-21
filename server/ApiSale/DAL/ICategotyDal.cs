using ApiSale.Models;
using ApiSale.Models.ModelDTO;

namespace ApiSale.DAL
{
    public interface ICategotyDal
    {
        void AddAsync(Categorya categorya);
        void RemoveAsync(int id);
        public Task<List<Categorya>> GetCategoryas();


    }
}
