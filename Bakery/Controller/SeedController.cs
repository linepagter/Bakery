using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Bakery.Data;
using Microsoft.EntityFrameworkCore;
using Bakery.Models.Csv;


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
        
        // [HttpPut(Name = "Seed")]
        // [ResponseCache(NoStore = true)]
        // public async Task<IActionResult> Put()
        // {
        //     //Setup
        //     var config = new CsvConfiguration(CultureInfo.GetCultureInfo("pt-BR"))
        //     {
        //         HasHeaderRecord = true,
        //         Delimiter = ";",
        //     };
        //     using var reader = new StreamReader(
        //         System.IO.Path.Combine(_environment.ContentRootPath, "Data/Bakery.csv"));
        //     using var csv = new CsvReader(reader, config);
        //     var existingBatch = await _context.Batch
        //         .ToDictionaryAsync(batch => batch.BatchId);
        //     var existingIngredients = await _context.Ingredients
        //         .ToDictionaryAsync(ingredients => ingredients.IngredientsId);
        //     var existingList_of_bakinggoods = await _context.ListOfBakingGoods
        //         .ToDictionaryAsync(listofbakinggoods => listofbakinggoods.ListId);
        //     var existingOrder = await _context.Orders
        //         .ToDictionaryAsync(orders => orders.OrderId);
        //     var existingPackage = await _context.Packages
        //         .ToDictionaryAsync(package => package.Trackid);
        //     var existingStock = await _context.Stocks
        //         .ToDictionaryAsync(stock => stock.StockId);
        //     var now = DateTime.Now;
        //
        //     //Execute
        //     var records = csv.GetRecords<BakeryRecord>();
        //     var skippedRows = 0;
        //     foreach (var record in records)
        //     {
        //         if (!record.Id.HasValue
        //             || string.IsNullOrEmpty(record.Name)
        //             ||existingBatch.ContainsKey(record.Id.Value))
        //         { 
        //             skippedRows++;
        //             continue; 
        //         }
        //             
        //     }
        //
        // }
            
    }
}
