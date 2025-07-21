using ApiSale.Models.ModelDTO;
using ApiSale.Models;

namespace ApiSale.DAL
{
    public interface IOrderDal
    {
        Task<string> AddOrderAsync(OrderDTO orderDTO);
        Task<string> DeleteOrderAsync(int OrderId);
        public Task<List<Order>> GetUserCart(int userId);
        public Task<string> ToBuy(int userId);
        public Task<List<User>> GetOrderByGift(int giftId);
        public Task<List<Gift>> PopularGift();
        public Task<List<User>> GerUserOrders();
        public Task<int> Profit();

    }
    }
