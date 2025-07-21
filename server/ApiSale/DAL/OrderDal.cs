using ApiSale.Models;
using ApiSale.Models.ModelDTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiSale.DAL
{
    public class OrderDal : IOrderDal
    {
        private readonly ChainaSaleDBContext dBContext;
        private readonly IMapper mapper;

        public OrderDal(ChainaSaleDBContext dBContext,IMapper mapper)
        {
            this.dBContext = dBContext;
            this.mapper = mapper;
        }
        public async Task<string> AddOrderAsync(OrderDTO orderDTO)
        {
            var NewOrder = mapper.Map<Order>(orderDTO);
            dBContext.Add(NewOrder);
            await dBContext.SaveChangesAsync();
            return "order!";
        }

        public async Task<string> DeleteOrderAsync(int orderId)
        {
           var order= await dBContext.Order.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"gift {orderId} not fount");
            }
            else
            {
                dBContext.Order.Remove(order);
                await dBContext.SaveChangesAsync();
                return $"order {orderId} not delete!";
            }
        }

        public async Task<List<Order>> GetUserCart(int userId)
        {
            List<Order> orderList = new List<Order>();
            orderList = await dBContext.Order.Where(o => o.UserId == userId).Include(u=>u.Gift).ToListAsync();
            return orderList;
        }
        public async Task<string> ToBuy(int userId)
        {
            var orders = await dBContext.Order.Where(o => o.UserId == userId).ToListAsync();
            foreach (var item in orders)
            {
                item.IsDraft = true;
            }
            await dBContext.SaveChangesAsync();
            return "orderrrrrr";

        }
        public async Task<List<User>> GetOrderByGift(int giftId)
        {

            List<User> userList = new List<User>(); 
            var orders = await dBContext.Order.Where(o => o.GiftId == giftId).Where(o=>o.IsDraft==true).Include(u => u.User).ToListAsync();
            foreach (var item in orders)
            {
                userList.Add(item.User);
            }
            return userList;
           
        }
        public async  Task<List<User>> GerUserOrders()
        {
            var userList = new List<User>();    
            var list=await dBContext.Order.Where(o=>o.IsDraft==true).Include(u=>u.User).Include(o=>o.Gift).ToListAsync();
            var uniqueUsers = list.Select(o => o.User).Where(u => u != null).Distinct().ToList();
            return uniqueUsers;



        }
        public async Task<List<Gift>> PopularGift()
        {
           List<Gift>list = new List<Gift>();
            var groupedByGift = await dBContext.Order.Include(o => o.Gift).Where(o=>o.IsDraft==true).GroupBy(o => o.Gift).Select(group => new
            {
                Gift = group.Key,
                sumOrders = group.Count()
            }).OrderByDescending(o=>o.sumOrders).ToListAsync();
           
            return groupedByGift.Select(g=>g.Gift).ToList();
        }

        public async Task<int> Profit()
        {
            int sum = await dBContext.Order.Where(o => o.IsDraft == true).Include(o => o.Gift).SumAsync(o => o.Gift.TicketPrice);
            return sum;
        }
         
    }
}
