using ApiSale.DAL;
using ApiSale.Models;
using ApiSale.Models.ModelDTO;

namespace ApiSale.BL
{
    public class OrderService : IOrderSevice
    {
        private readonly IOrderDal orderDal;

        public OrderService(IOrderDal orderDal)
        {
            this.orderDal = orderDal;
        }

      

        public async Task<string> AddOrderAsync(OrderDTO orderDTO)
        {
            await orderDal.AddOrderAsync(orderDTO);

            return "add";
        }

        public async Task<string> DeleteOrderAsync(int orderId)
        {
           return await orderDal.DeleteOrderAsync(orderId);
        }

        public async Task<List<User>> GerUserOrders()
        {
            return await orderDal.GerUserOrders();
        }

        public async Task<List<User>> GetOrderByGift(int giftId)
        {
           return await orderDal.GetOrderByGift(giftId);
        }

        public async Task<List<Order>> GetUserCart(int userId)
        {

            return await orderDal.GetUserCart(userId);
        }

        public async Task<List<Gift>> PopularGift()
        {
            return await orderDal.PopularGift();
        }

        public Task<int> Profit()
        {
            return orderDal.Profit();
        }

        public async Task<string> ToBuy(int userId)
        {
            return await orderDal.ToBuy(userId);
        }
    }
}
