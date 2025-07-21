using ApiSale.DAL;
using ApiSale.Models;

namespace ApiSale.BL
{
    public class CategoryService:ICategoryService

    {
        private readonly ICategotyDal categotyDal;

        public CategoryService(ICategotyDal categotyDal)
        {
            this.categotyDal = categotyDal ?? throw new ArgumentNullException(nameof(categotyDal));
        }

        public async void AddAsync(Categorya categorya)
        {
            categotyDal.AddAsync(categorya);
        }

        public async Task<List<Categorya>> GetCategoryas()
        {
            return await categotyDal.GetCategoryas();
        }

        public async void RemoveAsync(int id)
        {
            categotyDal.RemoveAsync(id);
        }
    }
}
