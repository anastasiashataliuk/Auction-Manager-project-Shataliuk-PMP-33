using DAL.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.Concrete
{
    public class AuctionDAL : IAuctionDal
    {
        private readonly string _connectionString;

        public AuctionDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public List<Auction> GetAll()
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT auction_id, start_date, end_date, starting_price, buyout_price, status FROM TblAuction";
                    using (var reader = command.ExecuteReader())
                    {
                        var auctions = new List<Auction>();
                        while (reader.Read())
                        {
                            auctions.Add(new Auction
                            {
                                Auction_Id = Convert.ToInt32(reader["auction_id"]),
                                Start_Date = Convert.ToDateTime(reader["start_date"]),
                                End_Date = Convert.ToDateTime(reader["end_date"]),
                                Starting_Price = Convert.ToDecimal(reader["starting_price"]),
                                Buyout_Price = Convert.ToDecimal(reader["buyout_price"]),
                                Status = Convert.ToString(reader["status"]),
                            });
                        }
                        return auctions;
                    }
                }
            }
        }

        public Auction Insert(Auction auction)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO TblAuction (start_date, end_date, starting_price, buyout_price, status) OUTPUT INSERTED.auction_id VALUES (@start_date, @end_date, @starting_price, @buyout_price, @status)";
                    command.Parameters.AddWithValue("@start_date", auction.Start_Date);
                    command.Parameters.AddWithValue("@end_date", auction.End_Date);
                    command.Parameters.AddWithValue("@starting_price", auction.Starting_Price);
                    command.Parameters.AddWithValue("@buyout_price", auction.Buyout_Price);
                    command.Parameters.AddWithValue("@status", auction.Status);

                    try
                    {
                        auction.Auction_Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert auction", ex);
                    }
                    return auction;
                }
            }
        }

        public Auction GetById(int id)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT auction_id, start_date, end_date, starting_price, buyout_price, status FROM TblAuction WHERE auction_id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Auction
                            {
                                Auction_Id = Convert.ToInt32(reader["auction_id"]),
                                Start_Date = Convert.ToDateTime(reader["start_date"]),
                                End_Date = Convert.ToDateTime(reader["end_date"]),
                                Starting_Price = Convert.ToDecimal(reader["starting_price"]),
                                Buyout_Price = Convert.ToDecimal(reader["buyout_price"]),
                                Status = Convert.ToString(reader["status"]),
                            };
                        }
                    }
                }
            }
            return null; // Якщо аукціон не знайдено
        }

        public void Update(Auction auction)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE TblAuction SET start_date = @start_date, end_date = @end_date, starting_price = @starting_price, buyout_price = @buyout_price, status = @status WHERE auction_id = @id";
                    command.Parameters.AddWithValue("@id", auction.Auction_Id);
                    command.Parameters.AddWithValue("@start_date", auction.Start_Date);
                    command.Parameters.AddWithValue("@end_date", auction.End_Date);
                    command.Parameters.AddWithValue("@starting_price", auction.Starting_Price);
                    command.Parameters.AddWithValue("@buyout_price", auction.Buyout_Price);
                    command.Parameters.AddWithValue("@status", auction.Status);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Update failed. Auction not found.");
                    }
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM TblAuction WHERE auction_id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Delete failed. Auction not found.");
                    }
                }
            }
        }
    }
}
