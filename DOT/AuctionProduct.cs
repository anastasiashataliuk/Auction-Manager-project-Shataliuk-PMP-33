namespace DTO
{
    public class AuctionProduct
    {
        public int AuctionProduct_Id { get; set; } // Ідентифікатор зв'язку аукціон-продукт

        public int Auction_Id { get; set; } // Ідентифікатор аукціону

        public int Product_Id { get; set; } // Ідентифікатор продукту

        public decimal Quantity { get; set; } // Кількість продукту (numeric(18))
    }
}
