using ApiSale.Models.ModelDTO;
using ApiSale.Models;

namespace ApiSale.BL
{
    public interface IDonorService
    {
        Task<List<Donor>> GetAsync();

        Task AddDonorAsync(DonorDTO donor);

        Task UpdateDonorAsync(int id, DonorDTO donorDTO);

        Task RemoveAsync(int id);

        Task<List<Donor>> GetByNameAsync(int id);
        Task<Donor> GetById(int id);
        Task<List<Gift>> GetGiftsByDonor(int id);
        Task<List<Donor>> SearchDonor(string parm);


    }
}
