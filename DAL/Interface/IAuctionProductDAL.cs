using System.Collections.Generic;
using DTO;

namespace DAL.Interface
{
    public interface IAuctionProductDAL
    {
        // Додаємо продукт до аукціону з кількістю
        void AddProductToAuction(int auctionId, int productId, decimal quantity);

        // Видаляємо продукт з аукціону
        void RemoveProductFromAuction(int auctionId, int productId);

        // Оновлюємо кількість продукту в аукціоні
        void UpdateProductQuantityInAuction(int auctionId, int productId, decimal quantity);

    }
}
