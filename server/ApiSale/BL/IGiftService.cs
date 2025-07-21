using ApiSale.Models.ModelDTO;
using ApiSale.Models;

namespace ApiSale.BL
{
    public interface IGiftService
    {
        Task<List<GiftDTO>> GetGiftAsync();

        Task<Gift> AddGiftAsync(GiftDTO giftDTO);

        Task<Gift> UpdateGift(int id, GiftDTO giftDTO);

        Task RemoveGift(int id);

        Task <Gift> GetGiftByIdAsync(int id);
        Task<string> GetDonorByGift(int id);
        Task<List<GiftDTO>> SearchGiftByNameGift(string parm);
        Task<List<GiftDTO>> SearchGiftByDonorName(string parm);

        public Task<User> RandomGift(int giftId);

    }
}

