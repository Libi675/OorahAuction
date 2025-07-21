using ApiSale.Models;
using ApiSale.Models.ModelDTO;

namespace ApiSale.DAL
{
    public interface IDonorDal
    {
        Task<List<Donor>> GetAsync();
       
        Task AddDonorAsync(DonorDTO donor);

        Task UpdateDonorAsync(int id,DonorDTO donorDTO);

        Task RemoveDonorAsync(int id);

        Task<List<Donor>> GetByNameAsync(int id);

        Task<Donor> GetById(int id);

        Task<List<Gift>> GetGiftsByDonor(int id);

        Task<List<Donor>> SearchDonor(string parm);
        //לברר על סינונים
        //DonorDTO GetByNameDonorAsync(string name);

    }
}
