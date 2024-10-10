using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL.Interface;
using DTO;

namespace DAL.Concrete
{
    public class AuctionProductDAL : IAuctionProductDAL
    {
        private readonly string _connectionString;

        public AuctionProductDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Метод для додавання продукту до аукціону з вказаною кількістю
        public void AddProductToAuction(int auctionId, int productId, decimal quantity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO AuctionProducts (AuctionId, ProductId, Quantity) VALUES (@AuctionId, @ProductId, @Quantity)", connection))
                {
                    command.Parameters.AddWithValue("@AuctionId", auctionId);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@Quantity", quantity);  // Додаємо кількість
                    command.ExecuteNonQuery();
                }
            }
        }

        // Метод для видалення продукту з аукціону
        public void RemoveProductFromAuction(int auctionId, int productId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM AuctionProducts WHERE AuctionId = @AuctionId AND ProductId = @ProductId", connection))
                {
                    command.Parameters.AddWithValue("@AuctionId", auctionId);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Метод для оновлення кількості продукту в аукціоні
        public void UpdateProductQuantityInAuction(int auctionId, int productId, decimal newQuantity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE AuctionProducts SET Quantity = @Quantity WHERE AuctionId = @AuctionId AND ProductId = @ProductId", connection))
                {
                    command.Parameters.AddWithValue("@AuctionId", auctionId);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@Quantity", newQuantity);  // Оновлюємо кількість
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
