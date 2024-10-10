using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DALEF.Models
{
    [Table("tblAuctionProduct")]
    public partial class TblAuctionProduct
    {
        public int AuctionProduct_Id { get; set; } // Унікальний ідентифікатор для зв'язку аукціон-продукт

        public int Auction_Id { get; set; } // Ідентифікатор аукціону

        public int Product_Id { get; set; } // Ідентифікатор продукту

        public decimal Quantity { get; set; } // Кількість продукту в аукціоні (додано)

        // Віртуальні властивості для навігації до аукціону та продукту
        public virtual TblAuction Auction1 { get; set; }
        public virtual TblProduct Product1 { get; set; }
    }
}
