using ApiSale.Models.ModelDTO;
using ApiSale.Models;

namespace ApiSale.DAL
{
    public interface IGiftDal
    {
        Task<List<Gift>> GetGiftAsync();


        Task<Gift> AddGiftAsync(GiftDTO giftDTO);

        Task<Gift> UpdateGift(int id, GiftDTO giftDTO);

        Task RemoveGift(int id);

        Task <Gift> GetGiftByIdAsync(int id);

        Task<string> GetDonorByGift(int id);

        Task<List<Gift>> SearchGiftByNameGift(string parm);
        Task<List<Gift>> SearchGiftByDonorName(string parm);
        public Task<User> RandomGift(int giftId);

    }
}
