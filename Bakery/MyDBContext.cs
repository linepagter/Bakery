using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bakery.Models;
using Microsoft.EntityFrameworkCore;

namespace Bakery
{
    public class MyDbContext : DbContext
    {
        private const string DbName = "BakeryDB";
        private const string ConnectionString = $"Data Source=localhost;Initial Catalog={DbName};User ID=SA;Password=<YourStrong@Passw0rd>;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        internal DbSet<Batch> Batch{ get; set; }
        internal DbSet<Has> Has{ get; set; }
        internal DbSet<Ingredients> Ingredients { get; set; }
        internal DbSet<List_of_baking_goods> ListOfBakingGoods { get; set; }
        internal DbSet<Made_in> madein { get; set; }
        internal DbSet<Order> Orders{ get; set; }
        internal DbSet<Package> Packages{ get; set; }
        internal DbSet<Stock> Stocks{ get; set; }
        internal DbSet<Uses> Uses{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(ConnectionString);
        
        //order.hasMany(x 0> x.PackageID)
    }
}