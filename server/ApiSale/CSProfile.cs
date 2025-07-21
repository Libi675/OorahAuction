using ApiSale.Models.ModelDTO;
using ApiSale.Models;
using AutoMapper;

namespace ApiSale
{
    public class CSProfile:Profile
    {
        public CSProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            //CreateMap<GiftDTO, Gift>().ForMember(dest => dest.DonorsGift.FullName, o => o.MapFrom(src => src.DonorsGift.FullName)).ReverseMap();
            //
            CreateMap<Gift, GiftDTO>().ForMember(dest => dest.DonorNameGift, o => o.MapFrom(src => src.DonorsGift != null ? src.DonorsGift.FullName : ""))
                .ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<DonorDTO, Donor>().ReverseMap();


        }

    }
}
