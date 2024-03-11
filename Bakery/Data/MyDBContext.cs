using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bakery.Models;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Has>()
                .HasKey(i => new { i.StockId, i.IngredientsId });

            modelBuilder.Entity<Has>()
                .HasOne(x => x.Stock)
                .WithMany(y => y.Has)
                .HasForeignKey(f => f.Stock)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Has>()
                .HasOne(o => o.Ingredients)
                .WithMany(m => m.Has)
                .HasForeignKey(f => f.IngredientsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Uses>()
                .HasKey(i => new { i.IngredientsId, i.BatchId });

            modelBuilder.Entity<Uses>()
                .HasOne(x => x.Ingredients)
                .WithMany(y => y.Uses)
                .HasForeignKey(f => f.IngredientsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Uses>()
                .HasOne(o => o.Batch)
                .WithMany(m => m.Uses)
                .HasForeignKey(f => f.BatchId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Made_in>()
                .HasKey(i => new { i.OrderId, i.BatchId });

            modelBuilder.Entity<Made_in>()
                .HasOne(x => x.Order)
                .WithMany(y => y.MadeIns)
                .HasForeignKey(f => f.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Made_in>()
                .HasOne(o => o.Batch)
                .WithMany(m => m.MadeIns)
                .HasForeignKey(f => f.BatchId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Package>()
                .HasOne(o => o.Order)
                .WithMany(p => p.Packages)
                .HasForeignKey(f => f.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        //private const string DbName = "BakeryDB";
        //private const string ConnectionString = $"Data Source=localhost;Initial Catalog={DbName};User ID=SA;Password=<YourStrong@Passw0rd>;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public DbSet<Batch> Batch => Set<Batch>();
        public DbSet<Has> Has => Set<Has>();
        public DbSet<Ingredients> Ingredients => Set<Ingredients>();
        public DbSet<List_of_baking_goods> ListOfBakingGoods => Set<List_of_baking_goods>();
        public DbSet<Made_in> MadeIns => Set<Made_in>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Package> Packages => Set<Package>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<Uses> Uses => Set<Uses>();

        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //     => options.UseSqlServer(ConnectionString);
    }
}