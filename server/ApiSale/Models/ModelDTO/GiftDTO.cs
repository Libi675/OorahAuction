namespace ApiSale.Models.ModelDTO
{
    public class GiftDTO
    {
        public int GiftId { get; set; }
        public string? GiftName { get; set; }


        public int TicketPrice { get; set; }
        public int? DonorId { get; set; }
        public string? DonorNameGift { get; set; }
        public int? CategoryaId { get; set; }
        public string image { get; set; }

    }
}
