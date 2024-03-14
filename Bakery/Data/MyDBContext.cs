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

            

            modelBuilder.Entity<BatchIngredient>()
                .HasKey(i => new { i.IngredientsId, i.BatchId });

            modelBuilder.Entity<BatchIngredient>()
                .HasOne(x => x.Ingredients)
                .WithMany(y => y.BatchIngredient)
                .HasForeignKey(f => f.IngredientsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BatchIngredient>()
                .HasOne(o => o.Batch)
                .WithMany(m => m.BatchIngredient)
                .HasForeignKey(f => f.BatchId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Package>()
                .HasOne(o => o.Order)
                .WithMany(p => p.Packages)
                .HasForeignKey(f => f.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<ListOfBakingGoods>()
                .HasOne(o => o.Order)
                .WithOne(l => l.ListOfBakingGoods)
                .HasForeignKey<ListOfBakingGoods>(f => f.OrdreId)
                .HasPrincipalKey<Order>(p => p.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        
        public DbSet<Batch> Batch => Set<Batch>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<ListOfBakingGoods> ListOfBakingGoods => Set<ListOfBakingGoods>();
       
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Package> Packages => Set<Package>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<BatchIngredient> BatchIngredient => Set<BatchIngredient>();

    }
}