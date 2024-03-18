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
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    public class SeedController:ControllerBase
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
            var order1 = new Order { DeliveryPlace = "Finlandsgade 17", DeliveryDate = new DateTime(2024,08,12,08,00,00) };
            
            var package1 = new Package { Order=order1};
            
            _context.Orders.Add(order1);
            _context.Packages.Add(package1);
            _context.SaveChanges();

            return new JsonResult(new
            {
                Package = _context.Packages.Count(),
                Order = _context.Orders.Count()
                
            });


        }

         
    }
}
