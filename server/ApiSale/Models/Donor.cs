namespace ApiSale.Models
{
    public class Donor
    {
        public int DonorId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IEnumerable<Gift>? GiftList { get; set; }
    }
}
