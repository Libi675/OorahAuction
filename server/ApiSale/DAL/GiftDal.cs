using ApiSale.Models;
using ApiSale.Models.ModelDTO;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ApiSale.DAL
{
    public class GiftDal : IGiftDal
    {
        private readonly IMapper mapper;
        private readonly ChainaSaleDBContext dBContext;

        public GiftDal(IMapper mapper,ChainaSaleDBContext dBContext)
        {
            this.mapper = mapper;
            this.dBContext = dBContext;
        }
        public async Task<List<Gift>> GetGiftAsync()
        {
            return await dBContext.Gift.Include(g => g.DonorsGift).Include(g=>g.Categorya).ToListAsync();
        }


        public async Task<Gift> GetGiftByIdAsync(int id)
        {
            var gift = await dBContext.Gift.FirstOrDefaultAsync(g => g.GiftId == id);
            if (gift == null)
            {
                throw new KeyNotFoundException($"Gift {id} not fount");
            }

            return gift;
        }

public async Task<Gift> AddGiftAsync(GiftDTO giftDTO)
        {
            var NewGift = mapper.Map<Gift>(giftDTO);
            var donor = await dBContext.Donor.FirstOrDefaultAsync(g => g.DonorId == NewGift.DonorId);
            var nameGift = await dBContext.Gift.FirstOrDefaultAsync(u => u.GiftName == giftDTO.GiftName);
            if (donor == null)
            {
                throw new KeyNotFoundException($"donor {NewGift.DonorId} not exsist");
            }
            if (nameGift!= null)
            {
                throw new DuplicateWaitObjectException("this gift already exist!");
            }
            dBContext.Add(NewGift);
            await dBContext.SaveChangesAsync();
            return NewGift;
        }

        
        public async Task RemoveGift(int id)
        {
            var gift = await dBContext.Gift.FirstOrDefaultAsync(g=>g.GiftId == id);
            if (gift == null)
            {
                throw new KeyNotFoundException($"gift {id} not fount");
            }
            else { 
                dBContext.Gift.Remove(gift);
                await dBContext.SaveChangesAsync();
                    }
           
        }

        public async Task<Gift> UpdateGift(int id, GiftDTO giftDTO)
        {
            var gift =await dBContext.Gift.FirstOrDefaultAsync(g => g.GiftId == id);
            var giftUpdate = mapper.Map<Gift>(giftDTO);
            giftUpdate.GiftId = id;
            dBContext.Gift.Entry(gift).CurrentValues.SetValues(giftUpdate);
            await dBContext.SaveChangesAsync();
           return giftUpdate;

        }

        public async Task<string> GetDonorByGift(int id)
        {
            var gift = await dBContext.Gift.FirstOrDefaultAsync(g => g.GiftId == id);
            if (gift == null)
            {
                throw new KeyNotFoundException($"gift {id} not fount");
            }
            var nameDonor = await dBContext.Donor.FirstOrDefaultAsync(d => d.DonorId == gift.DonorId);
            string name = nameDonor.FullName;
            return name;
        }

        public async Task<List<Gift>> SearchGiftByNameGift(string name)
        {
            //var donorList = chainaSaleDBContext.Donor.Where(d => d.Email.Contains(parm)+(d=> d.E)
            List<Gift> GiftList = await (from g in dBContext.Gift
                                   where (g.GiftName).Contains(name) 
                                   select g).ToListAsync();
            if (GiftList == null)
            {
                throw new KeyNotFoundException($"Gift {name} not fount");
            }
            return GiftList;
        }
        public async Task<List<Gift>> SearchGiftByDonorName(string donorname)
        {
            
             var donorList = await (from d in dBContext.Donor
                                  where (d.FullName).Contains(donorname)
                                  select d).ToListAsync();

            List<Gift> listGift=new List<Gift>();
            foreach (var donor in donorList)
            {
                listGift = await dBContext.Gift.Where(g => g.DonorId == donor.DonorId).ToListAsync();  
            }
            if (listGift == null)
            {
                throw new KeyNotFoundException($"Gift {donorname} not fount");
            }
            return listGift;
        }


        //public async Task<User> Random(int presentId)
        //{
        //    Random random = new Random();
        //    var purchases = await chinaSaleDBContex.Purchases
        //        .Where(p => p.PresentId == presentId)
        //        .Include(p => p.User)
        //        .ToListAsync();

        //    if (purchases.Count == 0)
        //    {
        //        return null;
        //    }
        //    var randomPurchase = purchases[random.Next(purchases.Count)];
        //    return randomPurchase.User;
        //}(
        public async Task<User> RandomGift( int giftId)
        {
            Random random = new Random();
            var purchases = await dBContext.Order.Where(o => o.IsDraft == true).Where(o => o.GiftId == giftId).Include(o => o.User).ToListAsync();
            if (purchases.Count == 0)
            {
              return null;
            }
            var randomPurchase = purchases[random.Next(purchases.Count)];
            //var gift = await dBContext.Gift.FirstOrDefaultAsync(g => g.GiftId == giftId);
            //gift.isRuffel = true;
            //await dBContext.SaveChangesAsync();
            return randomPurchase.User;

        }

    }
}
