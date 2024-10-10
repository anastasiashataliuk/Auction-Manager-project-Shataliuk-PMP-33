using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DALEF.Models
{
    [Table("tblAuction")]
    public partial class TblAuction
    {
        public int Auction_Id { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public decimal Starting_Price { get; set; }
        public decimal Buyout_Price { get; set; }
        public string Status { get; set; }

        // Віртуальна властивість для навігації до продуктів, які пов'язані з цим аукціоном
        public virtual ICollection<TblAuctionProduct> AuctionProduct1 { get; set; } = new List<TblAuctionProduct>(); // Додайте ініціалізацію

        public virtual ICollection<TblProduct> Product1 { get; set; } = new List<TblProduct>();
    }
}
