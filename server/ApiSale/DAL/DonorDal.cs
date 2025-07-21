using ApiSale.Models;
using ApiSale.Models.ModelDTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiSale.DAL
{
    public class DonorDal : IDonorDal
    {
        private readonly ChainaSaleDBContext chainaSaleDBContext;
        private readonly IMapper mapper;

        public DonorDal(ChainaSaleDBContext chainaSaleDBContext,IMapper mapper)
        {
            this.chainaSaleDBContext = chainaSaleDBContext ?? throw new ArgumentNullException(nameof(chainaSaleDBContext));
            this.mapper = mapper;
        }



        public async Task AddDonorAsync(DonorDTO donor)
        {
            var donorNew = mapper.Map<Donor>(donor);
            chainaSaleDBContext.Donor.AddAsync(donorNew);
            await chainaSaleDBContext.SaveChangesAsync();
        }


        public async Task<List<Donor>> GetAsync()
        {
            return await chainaSaleDBContext.Donor.ToListAsync();
        }

        public async Task<Donor> GetById(int id)
        {
            var donor = await chainaSaleDBContext.Donor.FirstOrDefaultAsync(d => d.DonorId == id);
            if (donor == null)
            {
                throw new KeyNotFoundException($"donor {id} not fount");
            }

            return donor;
        }

        public async Task RemoveDonorAsync(int id)
        {
            var donor = await chainaSaleDBContext.Donor.FirstOrDefaultAsync(d => d.DonorId == id);
            if (donor == null)
            {
                throw new KeyNotFoundException($"donor {id} not fount");
            }
            chainaSaleDBContext.Donor.Remove(donor);
            await chainaSaleDBContext.SaveChangesAsync();
        }

        public Task<List<Donor>> GetByNameAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<List<Gift>> GetGiftsByDonor(int id)
        {
        
           var giftList =await chainaSaleDBContext.Gift.Where(g => g.DonorId == id).ToListAsync();
            if (giftList.Count() == 0)
            {
                //שינוי
                throw new ArgumentException($"this donor: {id} not have gift");
            }
            else
            {
                return giftList;
            }
        }

        

        public async Task UpdateDonorAsync(int id,DonorDTO donorDTO)
        {
            var donor = await chainaSaleDBContext.Donor.FirstOrDefaultAsync(d => d.DonorId == id);
            var updateDonor = mapper.Map<Donor>(donorDTO);
            updateDonor.DonorId = id;
            chainaSaleDBContext.Donor.Entry(donor).CurrentValues.SetValues(updateDonor);
            await chainaSaleDBContext.SaveChangesAsync();
           
        }

        public async Task<List<Donor>> SearchDonor(string parm)
        {
            //var donorList = chainaSaleDBContext.Donor.Where(d => d.Email.Contains(parm)+(d=> d.E)
            var donorList =  await (from d in chainaSaleDBContext.Donor
                             where (d.FullName).Contains(parm) || (d.Email).Contains(parm) || (d.Phone).Contains(parm)
                             select d).ToListAsync();
            return donorList;


        }
    }
}
