namespace ApiSale.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int GiftId { get; set; }
        public Gift? Gift { get; set; }
        public User? User { get; set; }
        public bool IsDraft { get; set; } = false;
    }
}
