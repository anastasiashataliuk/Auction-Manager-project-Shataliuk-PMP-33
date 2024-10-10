using AutoMapper;
using DAL.Concrete;
using DAL.Interface;
using DALEF.Context;
using DALEF.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DALEF.Concrete
{
    public class AuctionProductDalEf : IAuctionProductDAL
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public AuctionProductDalEf(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public void AddProductToAuction(int auctionId, int productId, decimal quantity)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                // Додавання зв'язку продукту та аукціону з кількістю
                var auctionProduct = new TblAuctionProduct
                {
                    Auction_Id = auctionId,
                    Product_Id = productId,
                    Quantity = quantity // Додаємо кількість
                };

                context.AuctionProduct1.Add(auctionProduct);
                context.SaveChanges();
            }
        }

        public void RemoveProductFromAuction(int auctionId, int productId)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var auctionProduct = context.AuctionProduct1
                    .FirstOrDefault(ap => ap.Auction_Id == auctionId && ap.Product_Id == productId);
                if (auctionProduct != null)
                {
                    context.AuctionProduct1.Remove(auctionProduct);
                    context.SaveChanges();
                }
            }
        }

        public List<Product> GetProductsInAuction(int auctionId)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                // Отримуємо всі продукти, пов'язані з аукціоном
                var products = context.AuctionProduct1
                    .Where(ap => ap.Auction_Id == auctionId)
                    .Include(ap => ap.Product1)  // Завантажуємо пов'язані продукти
                    .Select(ap => new { ap.Product1, ap.Quantity }) // Включаємо кількість
                    .ToList();

                // Мапимо продукти та кількість до DTO і повертаємо
                var productDtos = products.Select(p => _mapper.Map<Product>(p.Product1)).ToList();

                // Додатково можна повернути кількість продуктів, якщо потрібно
                // Наприклад, через додавання додаткового поля до DTO

                return productDtos;
            }
        }
        public void UpdateProductQuantityInAuction(int auctionId, int productId, decimal quantity)
        {
            // Реалізація логіки оновлення кількості продукту в аукціоні
            using (var context = new AuctiondbContext(_connectionString))
            {
                var auctionProduct = context.AuctionProduct1
                    .FirstOrDefault(ap => ap.Auction_Id == auctionId && ap.Product_Id == productId);

                if (auctionProduct == null)
                {
                    throw new Exception("AuctionProduct not found.");
                }

                auctionProduct.Quantity = quantity; // Оновлюємо кількість
                context.SaveChanges();
            }
        }

    }
}
