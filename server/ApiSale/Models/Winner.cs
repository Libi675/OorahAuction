namespace ApiSale.Models
{
    public class Winner
    {
        public int winnerId { get; set; }
        public int giftId { get; set; }
        public Gift Gift { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
    }
}
