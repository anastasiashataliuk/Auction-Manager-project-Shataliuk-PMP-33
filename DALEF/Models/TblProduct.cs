using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DALEF.Models;

[Table("tblProduct")]
public partial class TblProduct
{
    public int Product_Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Category { get; set; }

    public virtual ICollection<TblAuctionProduct> AuctionProduct1 { get; set; } = new List<TblAuctionProduct>(); // Додайте ініціалізацію

}
