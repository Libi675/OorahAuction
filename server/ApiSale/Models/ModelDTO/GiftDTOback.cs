namespace ApiSale.Models.ModelDTO
{
    public class GiftDTOback
    {
        public int GiftId { get; set; }
        public string GiftName { get; set; }
        public int TicketPrice { get; set; }
        public string? DonorNaneGift { get; set; }
        public int? DonorId { get; set; }
        
        public int? CategoryaId { get; set; }
        public string image { get; set; }
    }
}
