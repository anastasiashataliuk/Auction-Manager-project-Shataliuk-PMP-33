namespace DTO
{
    public class Auction
    {
        public int Auction_Id { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public decimal Starting_Price { get; set; }
        public decimal Buyout_Price { get; set; }
        public string Status { get; set; }
    }
}
