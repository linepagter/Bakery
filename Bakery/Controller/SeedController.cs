using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Bakery.Data;
using Microsoft.EntityFrameworkCore;


namespace Bakery.Controller
{
    [Route("[controller")]
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
            var config = new CsvConfiguration(CultureInfo.GetCultureInfo("pt-BR"))
            {
                HasHeaderRecord = true,
                Delimiter = ";",
            };
            using var reader = new StreamReader(
                System.IO.Path.Combine(_environment.ContentRootPath, "Data/Bakery.csv"));
            using var csv = new CsvReader(reader, config);
            var existingBatch = await _context.Batch
                .ToDictionaryAsync(batch => batch.Id);
            var existingIngredients = await _context.Ingredients
                .ToDictionaryAsync(ingredients => ingredients.Id);
            var existingList_of_bakinggoods = await _context.ListOfBakingGoods
                .ToDictionaryAsync(listofbakinggoods => listofbakinggoods.ListId);
            var existingOrder = await _context.Orders
                .ToDictionaryAsync(orders => orders.Id);
            var existingPackage = await _context.Packages
                .ToDictionaryAsync(package => package.Trackid);
            var existingStock = await _context.Stocks
                .ToDictionaryAsync(stock => stock.Id);

        }
            
    }
}
