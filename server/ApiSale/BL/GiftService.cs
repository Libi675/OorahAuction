using ApiSale.DAL;
using ApiSale.Models;
using ApiSale.Models.ModelDTO;
using AutoMapper;

namespace ApiSale.BL
{
    public class GiftService:IGiftService
    {
        private readonly IMapper mapper;
        private readonly IGiftDal giftDal;
        

        public GiftService(IMapper mapper,IGiftDal giftDal)
        {
            this.mapper = mapper;
            this.giftDal = giftDal;
           
        }

        public  async Task<Gift> AddGiftAsync(GiftDTO giftDTO)
        {
         return  await giftDal.AddGiftAsync(giftDTO);
        }

        public async Task<string> GetDonorByGift(int id) => await giftDal.GetDonorByGift(id);

        public async Task<List<GiftDTO>> GetGiftAsync()
        {
             
            var list = await giftDal.GetGiftAsync();
            var newlIST = new List<GiftDTO>();
            foreach (var gift in list)
            {
                newlIST.Add(mapper.Map<GiftDTO>(gift));

            }
            return newlIST;

        }

        public async Task<Gift> GetGiftByIdAsync(int id) =>await giftDal.GetGiftByIdAsync(id);

        public async Task<User> RandomGift(int giftId)
        {
            return await giftDal.RandomGift(giftId);
        }

        public async Task RemoveGift(int id)
        {
            await giftDal.RemoveGift(id);
        }

        public async Task<List<GiftDTO>> SearchGiftByDonorName(string parm)
        {
            var list = await giftDal.SearchGiftByDonorName(parm);
            var newlIST = new List<GiftDTO>();
            foreach (var gift in list)
            {
                newlIST.Add(mapper.Map<GiftDTO>(gift));

            }
            return newlIST;

    
        }

        public async Task<List<GiftDTO>> SearchGiftByNameGift(string parm)
        {
            var list= await giftDal.SearchGiftByNameGift(parm);
            var newlIST = new List<GiftDTO>();
            foreach (var gift in list)
            {
                newlIST.Add(mapper.Map<GiftDTO>(gift));
                
            }
            return newlIST;
        }

        public async Task<Gift> UpdateGift(int id, GiftDTO giftDTO)
        {
         return await giftDal.UpdateGift(id, giftDTO);
        }

    }
}
