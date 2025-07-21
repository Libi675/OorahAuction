using ApiSale.DAL;
using ApiSale.Models;
using ApiSale.Models.ModelDTO;
using AutoMapper;

namespace ApiSale.BL
{
    public class DonorService : IDonorService
    {
        private readonly IMapper mapper;
        private readonly IDonorDal donorDal;

        public DonorService(IMapper mapper, IDonorDal donorDal)
        {
            this.mapper = mapper;
            this.donorDal = donorDal;
        }
        public async Task AddDonorAsync(DonorDTO donor)
        {
           await donorDal.AddDonorAsync(donor);
        }

        public async Task<List<Donor>> GetAsync() =>

             //var donorDTO = mapper.Map < Task<ListDonorDTO>(Task<List<Donor>>)
             await donorDal.GetAsync();

        public async Task<Donor> GetById(int id) => await donorDal.GetById(id);

        public async Task<List<Donor>> GetByNameAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Gift>> GetGiftsByDonor(int id) => await donorDal.GetGiftsByDonor(id);

        public async Task RemoveAsync(int id)
        {
            await donorDal.RemoveDonorAsync(id);
        }

        public async Task<List<Donor>> SearchDonor(string parm) => await donorDal.SearchDonor(parm);

        public async Task UpdateDonorAsync(int id, DonorDTO donorDTO)
        {
            await donorDal.UpdateDonorAsync(id, donorDTO);
        }

      
    }
}
