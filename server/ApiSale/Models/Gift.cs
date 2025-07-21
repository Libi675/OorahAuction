namespace ApiSale.Models
{
    public class Gift
    {
        public int GiftId { get; set; }
        public string GiftName { get; set; }
        public int TicketPrice { get; set; }
        public int? DonorId { get; set; }
        public int CategoryaId { get; set; }
        public string image { get; set; }
        //public bool isRuffel { get; set; }
        public Donor? DonorsGift { get; set; }
        public Categorya? Categorya { get; set;}
       


    }
}
