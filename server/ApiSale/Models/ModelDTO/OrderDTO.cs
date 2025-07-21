namespace ApiSale.Models.ModelDTO
{
    public class OrderDTO
    {
        public int UserId { get; set; }
        public int GiftId { get; set; }
        public bool IsDraft { get; set; } = false;
    }
}
