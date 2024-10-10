using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace DALEF.Models
{
    public partial class AuctionDBContext : DbContext
    {
        public AuctionDBContext()
        {
        }

        public AuctionDBContext(DbContextOptions<AuctionDBContext> options)
            : base(options)
        {
        }

        public AuctionDBContext(string connectionString)
        {
        }

        public virtual DbSet<TblAuction> Auction1 { get; set; }

        public virtual DbSet<TblProduct> Product1 { get; set; }

        public virtual DbSet<TblAuctionProduct> AuctionProduct1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("boombayaaa");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAuction>(entity =>
            {
                entity.HasKey(e => e.Auction_Id);

                entity.ToTable("tblAuction");

                entity.Property(e => e.Starting_Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Buyout_Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Status).HasMaxLength(20);
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.Product_Id);

                entity.ToTable("tblProduct");

                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Category).HasMaxLength(100);
            });

            modelBuilder.Entity<TblAuctionProduct>(entity =>
            {
                entity.HasKey(e => new { e.Auction_Id, e.Product_Id });
                entity.ToTable("tblAuctionProduct");

                entity.HasOne(d => d.Auction1)
                    .WithMany(p => p.AuctionProduct1)
                    .HasForeignKey(d => d.Auction_Id)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Product1)
                    .WithMany(p => p.AuctionProduct1)
                    .HasForeignKey(d => d.Product_Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
