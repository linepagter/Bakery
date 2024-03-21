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
             var order = new Order[]
            {
                new Order
                {
                    DeliveryPlace = "Finlandsgade 17", 
                    DeliveryDate = new DateTime(2024, 08, 12, 08, 00, 00)
                },
                new Order { DeliveryPlace = "Katrinebjergvej 2", DeliveryDate = new DateTime(2024, 10, 11, 10, 00, 00) }
            };

            var package = new Package[]
            {
                new Package { Order = order[0]},
                new Package { Order = order[0] }
            };
            

            var ingredients = new Ingredient[]
            {
                new Ingredient
                {
                    IngredientName = "Leftover cake",
                    
                },
                new Ingredient { IngredientName = "Raspberry jam" , StockQuantity = 10},
                new Ingredient { IngredientName = "Cocoa", StockQuantity = 200},
                new Ingredient { IngredientName = "Rum", StockQuantity = 123},
                new Ingredient { IngredientName = "coconut flakes", StockQuantity = 400},
                new Ingredient { IngredientName = "Sugar" , StockQuantity = 100000},
                new Ingredient { IngredientName = "Flour" , StockQuantity = 242000},
                new Ingredient { IngredientName = "Salt", StockQuantity =10000}

            };

           
            // var stock = new Stock[]
            // {
            //     new Stock { Quantity = 100000, Ingredient = ingredients[5] },
            //     new Stock { Quantity = 242000, Ingredient = ingredients[6] },
            //     new Stock { Quantity = 10000, Ingredient = ingredients[7]  }
            // };

            var listOfBakingGoods = new ListOfBakingGoods[]
            {
                new ListOfBakingGoods
                    { Order = order[0], Quantity = 300, Type = "Alexandertorte" },
                new ListOfBakingGoods
                    { Order = order[0], Quantity = 100, Type = "Buttercookies" },
                new ListOfBakingGoods
                    { Order = order[0], Quantity = 100, Type = "Studenterbrød" },
                new ListOfBakingGoods
                    { Order = order[0], Quantity = 200, Type = "Romkugler" },
                new ListOfBakingGoods
                    { Order = order[1], Quantity = 2900, Type = "Alexandertorte" },
                new ListOfBakingGoods
                    { Order = order[1], Quantity = 1499, Type = "Buttercookies" },
                new ListOfBakingGoods
                    { Order = order[1], Quantity = 800, Type = "Studenterbrød" },
                new ListOfBakingGoods
                    { Order = order[1], Quantity = 1800, Type = "Romkugler" }
            };

            var batch = new Batch[]
            {
                new Batch
                {
                    StartTime = new DateTime(2024, 06, 03, 10, 30, 00),
                    FinishTime = new DateTime(2024, 06, 03, 14, 30, 00),
                    TargetFinishTime = new DateTime(2024, 06, 03, 14, 20, 00),
                    //Order = new List<Order>(){ order[0], order[1]}
                },

                new Batch
                {
                    StartTime = new DateTime(2024, 07, 10, 12, 10, 00),
                    FinishTime = new DateTime(2024, 07, 10, 15, 30, 00),
                    TargetFinishTime = new DateTime(2024, 07, 10, 15, 00, 00)
                },

                new Batch
                {
                    StartTime = new DateTime(2024, 03, 01, 08, 00, 00),
                    FinishTime = new DateTime(2024, 03, 01, 09, 30, 00),
                    TargetFinishTime = new DateTime(2024, 03, 01, 09, 10, 00),
                }

            };


            var batchIngredient = new BatchIngredient[]
            {
                new BatchIngredient
                {
                     Batch = batch[0],
                     Ingredients = ingredients[0],
                     Quantity = 50000
                },
               
                new BatchIngredient
                {
                    Batch = batch[0],
                    Ingredients = ingredients[1],
                    Quantity = 30
                },
                new BatchIngredient
                {
                    Batch = batch[0],
                    Ingredients = ingredients[2],
                    Quantity = 20
                },
                new BatchIngredient
                {
                    Batch = batch[0],
                    Ingredients = ingredients[3],
                    Quantity = 30
                },
                new BatchIngredient
                {
                    Batch = batch[0],
                    Ingredients = ingredients[4],
                    Quantity = 0
                }
            };
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //_context.Stocks.AddRange(stock);  
                    //await _context.SaveChangesAsync();
                    _context.Ingredients.AddRange(ingredients);
                    await _context.SaveChangesAsync();
                    _context.Batch.AddRange(batch);
                    await _context.SaveChangesAsync();
                    _context.Orders.AddRange(order);
                    await _context.SaveChangesAsync();
                    _context.Packages.AddRange(package);
                    await _context.SaveChangesAsync();
                    _context.ListOfBakingGoods.AddRange(listOfBakingGoods); 
                    await _context.SaveChangesAsync();
                    _context.BatchIngredient.AddRange(batchIngredient);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    //transaction.Rollback();
                    throw;
                }
            }
            
            
            return new JsonResult(new
            {
                Package = _context.Packages.Count(),
                Order = _context.Orders.Count(),
                batch = _context.Batch.Count(),
                ingredients = _context.Ingredients.Count(),
                batchIngredient = _context.BatchIngredient.Count(),
                //stock = _context.Stocks.Count(),
                listOfBakingGoods = _context.ListOfBakingGoods.Count()
                
                
            });


        }

         
    }
}
