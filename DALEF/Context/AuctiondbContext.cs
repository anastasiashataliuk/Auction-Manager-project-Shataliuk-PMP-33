using DALEF.Models;
using Microsoft.EntityFrameworkCore;

namespace DALEF.Context
{
    public class AuctiondbContext : AuctionDBContext
    {
        private readonly string _connectionString;

        public AuctiondbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_connectionString);
    }
}