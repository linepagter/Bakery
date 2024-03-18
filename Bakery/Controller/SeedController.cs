using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Bakery.Data;
using Microsoft.EntityFrameworkCore;
using Bakery.Models;


namespace Bakery.Controller
{
    [Microsoft.AspNetCore.Components.Route("[controller")]
    [ApiController]
    public class SeedController
    {
         private readonly MyDbContext _context;
         private IWebHostEnvironment _environment;
        
        public SeedController(
            MyDbContext context,
            IWebHostEnvironment environment )
        {
            _context = context;
            _environment = environment;
        }

        [HttpPut(Name = "Seed")]
        [ResponseCache(NoStore = true)]

        public async Task<IActionResult> Put()
        {
            var skippedRows = 0;
            Console.WriteLine("Seeding data");
            var order1 = new Order { OrderId = 1, DeliveryPlace = "Finlandsgade 17", DeliveryDate = new DateTime(2024,08,12,08,00,00) };
            
            var package1 = new Package { TrackId = 12022, OrderId=1 };
            
            _context.Orders.Add(order1);
            _context.Packages.Add(package1);
            _context.SaveChanges();

            return new JsonResult(new
            {
                Package = _context.Packages.Count(),
                Order = _context.Orders.Count(),
                SkippedRows = skippedRows
                
            });


        }

         
    }
}
